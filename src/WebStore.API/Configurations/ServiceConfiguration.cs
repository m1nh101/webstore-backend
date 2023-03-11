using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Application.Common.Helpers;
using WebStore.Application.Services.Orders;
using WebStore.Application.Services.Users;

namespace WebStore.API.Configurations;

public static class ServiceConfiguration
{
    public static IServiceCollection RegisterService(this IServiceCollection services)
    {
        services.AddSingleton<IJwtGenerator, JwtGenerator>();

        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IExtractDataFromToken, UserSession>();


        return services;
    }
}
