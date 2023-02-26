using Microsoft.AspNetCore.Identity;
using WebStore.Domain.Entities;

namespace WebStore.Infrastructure.Database;

public static class SeedData
{
  public static IdentityRole[] CreateRoles()
  {
    return new IdentityRole[]
    {
      new() { Id = Guid.NewGuid().ToString(), Name = "admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
      new() { Id = Guid.NewGuid().ToString(), Name = "customer", NormalizedName = "CUSTOMER", ConcurrencyStamp = Guid.NewGuid().ToString() },
    };
  }

  public static User CreateAdminUser()
  {
    var user = new User();

    user.UserName = "admin";
    user.NormalizedUserName = "ADMIN";
    user.Email = "admin@admin.com";
    user.NormalizedEmail= "ADMIN@ADMIN.COM";
    user.EmailConfirmed = true;
    user.PasswordHash = new PasswordHasher<User>().HashPassword(user,"admin@adm1n");
    user.ConcurrencyStamp = Guid.NewGuid().ToString();

    return user;
  }
}
