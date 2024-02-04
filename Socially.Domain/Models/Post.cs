using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Socially.Domain.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Title { get; set; }

        public string TextBody { get; set; }

        public string? Username { get; set; }
    }
}
