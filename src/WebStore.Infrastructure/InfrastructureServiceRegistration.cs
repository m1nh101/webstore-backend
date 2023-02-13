using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Infrastructure.Database;

namespace WebStore.Infrastructure;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
  {

    var connectionString = configuration.GetConnectionString("WebStoreDevelopment");

    services.AddDbContext<WebStoreContext>(opt =>
    {
      opt.UseSqlServer(connectionString, setup =>
      {
        setup.MigrationsAssembly(typeof(InfrastructureServiceRegistration).Assembly.FullName);
        setup.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);

      });

      opt.LogTo(Console.WriteLine);
    });

    return services;
  }
}