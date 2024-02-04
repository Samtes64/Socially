using Socially.Domain.Models;

public interface IUserService

    {
        Task CreateUserAsync(User user);
        
    }