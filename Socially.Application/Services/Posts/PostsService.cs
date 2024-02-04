

// PostService.cs
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Socially.Domain.Models;
using Socially.Contracts.Post;


namespace Socially.Application.Services.Posts
{
    public class PostsService : IPostService
    {
        private readonly IMongoCollection<Post> _postsCollection;

        private readonly IOptions<DatabaseSettings> _dbSettings;

        public PostsService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _postsCollection = mongoDatabase.GetCollection<Post>(dbSettings.Value.PostsCollectionName);

        }

       public async Task CreatePostAsync(Post post)
        {
            await  _postsCollection.InsertOneAsync(post);
        }

        // Add other methods for handling posts as needed
    }
}
