using Socially.Application.Services.Users;
using Microsoft.Extensions.DependencyInjection;

namespace Socially.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services){

services.AddScoped<IUserService, UserService>();

return services;
    }
}
