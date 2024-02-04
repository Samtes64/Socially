using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Socially.Domain.Models;
using Microsoft.Extensions.Options;
using Socially.Infrastructure;




namespace Socially.Application.Services.Users
{

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        private readonly JwtService _jwtService;
        

        private readonly IOptions<DatabaseSettings> _dbSettings;

        public UserService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.UsersCollectionName);
            _jwtService = new JwtService("my_very_super_super_long_and_very_secretive_secret_key");
        }


        public async Task CreateUserAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            // Check if a user with the same username already exists
            var existingUser = await _usersCollection.Find(u => u.Username == user.Username).FirstOrDefaultAsync();
            if (existingUser != null)
            {
                throw new InvalidOperationException("User with the same username already exists.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            await _usersCollection.InsertOneAsync(user);
        }

        public async Task<(string token, User user)> LoginAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            if (string.IsNullOrEmpty(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            // Find the user with the given username
            var user = await _usersCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user == null)
            {
                // User with the given username does not exist
                throw new InvalidOperationException("User with the provided username does not exist.");
            }

            // Compare the provided password with the hashed password stored in the database
            if (BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                // Passwords match, generate JWT token
                var token = _jwtService.GenerateJwtToken(user);

                // Return the token and user
                return (token, user);
            }
            else
            {
                // Passwords do not match
                throw new InvalidOperationException("Provided password is incorrect.");
            }
        }

    }


}
