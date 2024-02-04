using Socially.Application.Services.Users;
using Socially.Domain.Models;


public interface IPostService

    {
        Task CreatePostAsync(Post post);
        
    }