using System.Net;
using Application.Tests.Mocks;
using Application.Tests.Setups;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework.Constraints;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Application.Common.Helpers;
using WebStore.Application.Services.Users;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Database;

namespace Application.Tests.IntergratedTests;

public class UserServiceTest
{
  private IUserService _userService = null!;

  [OneTimeSetUp]
  public async Task Setup()
  {
    var context = new DatabaseSetup().GetContext();

    var identitySetup = new IdentitySetup(context);

    var result = context.SaveChanges();

    var roles = context.Set<IdentityRole>().ToList();

    var roleManager = identitySetup.CreateRoleStore();
    var userManager = identitySetup.CreateUserStore();

    await roleManager.CreateAsync(new IdentityRole
    {
      Name = "customer",
      Id = Guid.NewGuid().ToString(),
      ConcurrencyStamp = Guid.NewGuid().ToString(),
      NormalizedName = "CUSTOMER"
    });

    var jwtGenerator = JwtGeneratorMock.MockJwtGenerator("this is token").Object;

    _userService = new UserService(userManager, jwtGenerator);
  }

  [Test]
  public async Task TestRegister(string username, string email, string password,
    HttpStatusCode status, UserAuthenticationResponse? data, object? errors,
    NullConstraint dataNull, NullConstraint errorNull)
  {
    var register = new UserRegistrationCredential
    {
      UserName = username,
      Email = email,
      Password = password
    };

    var result = await _userService.AddNew(register) as ResponseFactory;

    Assert.Multiple(() =>
    {
      Assert.That(result, Is.Not.Null);
      Assert.That(result!.StatusCode, Is.EqualTo(status));
      Assert.That(result.Data, dataNull);
      Assert.That(result.Errors, errorNull);
    });

    if(result!.Data != null)
    {
      var userData = (result.Data as UserAuthenticationResponse)!;

      Assert.Multiple(() =>
      {
        Assert.That(userData.Id, Is.Not.EqualTo(string.Empty));
        Assert.That(userData.Token, Is.EqualTo("this is token"));
        Assert.That(userData.FullName, Is.EqualTo(username));
      });
    }
  }
}