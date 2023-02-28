namespace WebStore.API.Configurations;

public static class CorsConfiguration
{
  public static IServiceCollection ConfigureCors(this IServiceCollection services)
  {
    services.AddCors(options =>
    {
      options.AddPolicy("__cors", policy =>
      {
        policy.AllowAnyHeader()
          .WithMethods("POST", "GET", "PUT", "PATCH", "DELETE")
          .AllowCredentials()
          .SetIsOriginAllowed(e => new Uri(e).Host == "localhost");
      });
    });

    return services;
  }
}
