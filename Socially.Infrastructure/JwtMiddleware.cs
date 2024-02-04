using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class JwtMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<JwtMiddleware> _logger;
    private readonly string _secretKey;

    public JwtMiddleware(RequestDelegate next, ILogger<JwtMiddleware> logger, string secretKey)
    {
        _next = next;
        _logger = logger;
        _secretKey = secretKey;
    }


    public async Task Invoke(HttpContext context)
    {
        string accessToken = context.Request.Headers["accessToken"];

        if (string.IsNullOrEmpty(accessToken))
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("User not logged in");
            return;
        }

        try
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            if (validatedToken != null && validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var username = jwtSecurityToken.Claims.First(x => x.Type == "unique").Value;

                // Add the username to HttpContext for use in controllers
                context.Items["Username"] = username;
            }

            await _next(context);
        }
        catch (SecurityTokenValidationException ex)
        {
            _logger.LogError("Token validation failed: " + ex.Message);
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Invalid token");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception occurred while validating token: " + ex.Message);
            context.Response.StatusCode = 500; // Internal Server Error
            await context.Response.WriteAsync("An error occurred");
        }
    }
}
