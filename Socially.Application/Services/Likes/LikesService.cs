// LikesService.cs
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Socially.Domain.Models;

namespace Socially.Application.Services.Likes
{
    public class LikesService : ILikesService
    {
        private readonly IMongoCollection<Like> _likesCollection;

        public LikesService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _likesCollection = mongoDatabase.GetCollection<Like>("Likes");
        }

        public async Task<bool> ToggleLikeAsync(string postId, string userId)
        {
            try
            {
                var foundLike = await _likesCollection.FindOneAndDeleteAsync(like =>
                    like.PostId == postId && like.UserId == userId);

                if (foundLike == null)
                {
                    await _likesCollection.InsertOneAsync(new Like
                    {
                        PostId = postId,
                        UserId = userId
                    });
                    return true; // Indicate that a like was added
                }
                return false; // Indicate that a like was removed
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
