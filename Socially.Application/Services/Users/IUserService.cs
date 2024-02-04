using Socially.Domain.Models;

public interface IUserService

    {
        Task CreateUserAsync(User user);
        Task<(string token, User user)> LoginAsync(string username, string password);
    }