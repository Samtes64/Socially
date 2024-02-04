using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System.Threading.Tasks;
using Socially.Domain.Models;
using Microsoft.Extensions.Options;

namespace Socially.Application.Services.Users
{

    public class UserService : IUserService
    {
        private readonly IMongoCollection<User> _usersCollection;
        
        private readonly IOptions<DatabaseSettings> _dbSettings;

        public UserService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _usersCollection = mongoDatabase.GetCollection<User>(dbSettings.Value.UsersCollectionName);
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
    }


}
