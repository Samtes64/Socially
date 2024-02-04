namespace Socially.Application.Services.Users;

public record AuthenticationResult(
    string Id,
    string UserName,
    string Token
    
    );