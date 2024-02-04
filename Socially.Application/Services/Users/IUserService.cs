using Socially.Domain.Models;

public interface IUserService

    {
        Task CreateUserAsync(User user);
        Task<User> LoginAsync(string username, string password);
    }