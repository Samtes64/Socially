namespace Socially.Contracts.User;

public record LoginResponse(
    string Id,
    string UserName,
    string Token
    
    );