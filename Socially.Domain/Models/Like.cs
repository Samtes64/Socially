using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socially.Domain.Models
{
    public class Like
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string UserId { get; set; } // Assuming a user ID for the like
        public string PostId { get; set; } // Reference to the parent post


    }
}
