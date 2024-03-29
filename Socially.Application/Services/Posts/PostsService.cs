

// PostService.cs
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Socially.Domain.Models;



namespace Socially.Application.Services.Posts
{
    public class PostsService : IPostService
    {
        private readonly IMongoCollection<Post> _postsCollection;
        private readonly IMongoCollection<Like> _likesCollection;

        private readonly IOptions<DatabaseSettings> _dbSettings;

        public PostsService(IOptions<DatabaseSettings> dbSettings)
        {
            _dbSettings = dbSettings;
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _postsCollection = mongoDatabase.GetCollection<Post>(dbSettings.Value.PostsCollectionName);
            _likesCollection = mongoDatabase.GetCollection<Like>(dbSettings.Value.LikesCollectionName);

        }

        public async Task CreatePostAsync(Post post)
        {
            await _postsCollection.InsertOneAsync(post);
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var posts = await _postsCollection.Find(_ => true).ToListAsync();

            foreach (var post in posts)
            {
                post.Likes = await GetLikesForPost(post.Id);
            }

            return posts;
        }

        public Post GetPostById(string id)
        {
            return _postsCollection.Find(post => post.Id == id).FirstOrDefault();
        }

        public List<Post> GetPostsByUserUsername(string userUsername)
        {
            return _postsCollection.Find(post => post.Username == userUsername).ToList();
        }

        public void UpdatePostTitle(string id, string newTitle)
        {
            _postsCollection.UpdateOne(post => post.Id == id, Builders<Post>.Update.Set("Title", newTitle));
        }

        public void UpdatePostText(string id, string newText)
        {
            _postsCollection.UpdateOne(post => post.Id == id, Builders<Post>.Update.Set("PostText", newText));
        }

        public void DeletePost(string id)
        {
            _postsCollection.DeleteOne(post => post.Id == id);
        }

        private async Task<List<Like>> GetLikesForPost(string postId)
        {
            return await _likesCollection.Find(like => like.PostId == postId).ToListAsync();
        }



        // Add other methods for handling posts as needed
    }
}
