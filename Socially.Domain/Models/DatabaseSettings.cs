namespace Socially.Domain.Models
{
    public class DatabaseSettings
    {
        public string? ConnectionString { get; set; }
        public string? DatabaseName { get; set; }
        public string? UsersCollectionName { get; set; }

        public string? PostsCollectionName { get; set; }

        public string? LikesCollectionName { get; set; }

        public string? CommentsCollectionName { get; set; }


    }
}