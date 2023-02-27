using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebStore.Application.Common.Helpers;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Database;

namespace Application.Tests.Setups;
#pragma warning disable CS8625
public class IdentitySetup
{
  private readonly WebStoreContext _context;

  public IdentitySetup(WebStoreContext context)
  {
    _context = context;
  }

  public UserManager<User> CreateUserStore()
  {
    var passwordHasher = new PasswordHasher<User>();


    var userManager = new UserManager<User>(new UserStore<User>(_context, new UserIdentityError()),
      null, passwordHasher, null, null, null, new UserIdentityError(), null, null);

    userManager.Options.Password = new PasswordOptions
    {
      RequireDigit = true,
      RequireNonAlphanumeric = false,
      RequiredLength = 6,
      RequireUppercase = false
    };

    return userManager;
  }

  public RoleManager<IdentityRole> CreateRoleStore()
  {
    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context), null, null, null, null);
    return roleManager;
  }
}