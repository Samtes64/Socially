using Socially.Application.Services.Users;
using Socially.Domain.Models;


public interface IPostService

    {
        Task CreatePostAsync(Post post);

        Task<IEnumerable<Post>> GetAllAsync();

        Post GetPostById(string id);

        void UpdatePostTitle(string id, string newTitle);

        void UpdatePostText(string id, string newText);

        void DeletePost(string id);

        List<Post> GetPostsByUserUsername(string userUsername);
        
    }