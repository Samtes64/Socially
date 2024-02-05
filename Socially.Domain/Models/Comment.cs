using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socially.Domain.Models
{
    public class Comment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Text { get; set; }
        public string PostId { get; set; } // Reference to the parent post

    }
}
