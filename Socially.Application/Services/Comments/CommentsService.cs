using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Socially.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Socially.Application.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IMongoCollection<Comment> _commentsCollection;

        public CommentsService(IOptions<DatabaseSettings> dbSettings)
        {
            var mongoClient = new MongoClient(dbSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(dbSettings.Value.DatabaseName);
            _commentsCollection = mongoDatabase.GetCollection<Comment>("Comments");
        }

        public async Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(string postId)
        {
            return await _commentsCollection.Find(comment => comment.PostId == postId).ToListAsync();
        }

        public async Task<Comment> CreateCommentAsync(Comment comment)
        {
            await _commentsCollection.InsertOneAsync(comment);
            return comment;
        }

        public async Task DeleteCommentAsync(string commentId)
        {
            await _commentsCollection.DeleteOneAsync(comment => comment.Id == commentId);
        }
    }
}