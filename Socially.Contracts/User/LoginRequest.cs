namespace Socially.Contracts.User;

public record LoginRequest(
    
    string Username,
    string Password
    
    );