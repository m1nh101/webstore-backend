using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    var errors = new UserIdentityError();

    var passwordHasher = new PasswordHasher<User>();
    var userValidators = new List<IUserValidator<User>>()
    {
      new UserValidator<User>(errors)
    };
    var passwordValidators = new List<IPasswordValidator<User>>()
    {
      new PasswordValidator<User>(errors)
    };


    var userManager = new UserManager<User>(new UserStore<User>(_context, errors),
      null, passwordHasher, userValidators, passwordValidators, null, errors, null, new Logger<UserManager<User>>(new LoggerFactory()));

    userManager.Options.Password = new PasswordOptions
    {
      RequireDigit = true,
      RequireNonAlphanumeric = false,
      RequiredLength = 6,
      RequireUppercase = false,
      RequireLowercase = false,
      RequiredUniqueChars = 0
    };

    userManager.Options.User.RequireUniqueEmail = true;

    return userManager;
  }

  public RoleManager<IdentityRole> CreateRoleStore()
  {
    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_context), null, null, null, null);
    return roleManager;
  }
}