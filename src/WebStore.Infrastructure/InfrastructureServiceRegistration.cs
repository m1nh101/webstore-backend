using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Helpers;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Database;

namespace WebStore.Infrastructure;

public static class InfrastructureServiceRegistration
{
  public static IServiceCollection ConfigureInfrastructureService(this IServiceCollection services, IConfiguration configuration)
  {

    var connectionString = configuration.GetConnectionString("WebStore");

    services.AddDbContext<WebStoreContext>(opt =>
    {
      opt.UseSqlServer(connectionString, setup =>
      {
        setup.MigrationsAssembly(typeof(InfrastructureServiceRegistration).Assembly.FullName);
        setup.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
      });

      opt.LogTo(Console.WriteLine);
    });

    services.AddIdentityCore<User>()
      .AddRoles<IdentityRole>()
      .AddEntityFrameworkStores<WebStoreContext>()
      .AddErrorDescriber<UserIdentityError>();

    services.AddScoped<IWebStoreContext, WebStoreContext>();

    var userManager = services.BuildServiceProvider().GetRequiredService<UserManager<User>>();
    var context = services.BuildServiceProvider().GetRequiredService<WebStoreContext>();
    
    MapAdminUser(userManager, context);

    return services;
  }

  private static void MapAdminUser(UserManager<User> userManager, WebStoreContext context)
  {
    if ((context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator)!.Exists())
    {
      Task.Run(async () =>
      {
        var isRoleMap = await context.Set<IdentityUserClaim<string>>().AnyAsync();

        if (!isRoleMap)
        {
          var user = await userManager.FindByNameAsync("admin");

          if (user == null) return;

          await userManager.AddToRoleAsync(user, "admin");
        }
      });
    }
  }
}