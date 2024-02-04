using Socially.Application.Services.Users;
using Socially.Domain.Models;

public interface IUserService

    {
        Task CreateUserAsync(User user);
        Task<AuthenticationResult> LoginAsync(string username, string password);
    }