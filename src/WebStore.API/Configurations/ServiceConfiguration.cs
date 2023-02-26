using WebStore.Application.Common.Contracts;
using WebStore.Application.Common.Helpers;
using WebStore.Application.Services.Users;

namespace WebStore.API.Configurations;

public static class ServiceConfiguration
{
  public static IServiceCollection RegisterService(this IServiceCollection services)
  {
    services.AddSingleton<JwtHelper>();

    services.AddScoped<IUserService, UserService>();

    return services;
  }
}
