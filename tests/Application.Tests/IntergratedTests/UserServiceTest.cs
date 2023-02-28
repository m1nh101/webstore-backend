using System.Net;
using Application.Tests.Mocks;
using Application.Tests.Setups;
using Application.Tests.Sources;
using Microsoft.AspNetCore.Identity;
using WebStore.Application.Common.Contracts;
using WebStore.Application.Common.Helpers;
using WebStore.Application.Services.Users;

namespace Application.Tests.IntergratedTests;

public class UserServiceTest
{
  private IUserService _userService = null!;
  private static List<object> _trueCase = UserSources.TrueCaseDataSources;
  private static List<object> _failCase = UserSources.FailCaseDataSource;
  private static List<object> _credential = UserSources.Credentials;

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
  [TestCaseSource(nameof(_trueCase))]
  [Order(1)]
  public async Task TestRegisterInTrueCase(string username, string email, string password, string fullName)
  {
    var register = new UserRegistrationCredential
    {
      UserName = username,
      Email = email,
      Password = password,
      FullName = fullName
    };

    var result = await _userService.AddNew(register) as ResponseFactory;

    Assert.Multiple(() =>
    {
      Assert.That(result, Is.Not.Null);
      Assert.That(result!.StatusCode, Is.EqualTo(HttpStatusCode.Created));
      Assert.That(result.Data, Is.Not.Null);
      Assert.That(result.Errors, Is.Null);

      var userData = (result.Data as UserAuthenticationResponse)!;

      Assert.That(userData.Id, Is.Not.EqualTo(string.Empty));
      Assert.That(userData.Token, Is.EqualTo("this is token"));
      Assert.That(userData.UserName, Is.EqualTo(username));
      Assert.That(userData.FullName, Is.EqualTo(fullName));
    });
  }

  [Test]
  [TestCaseSource(nameof(_failCase))]
  [Order(2)]
  public async Task TaskRegisterInFailCase(string username, string email, string password, string fullName, IEnumerable<string> errorFields)
  {
    var register = new UserRegistrationCredential
    {
      UserName = username,
      Email = email,
      Password = password,
      FullName = fullName
    };

    var result = await _userService.AddNew(register) as ResponseFactory;

    Assert.Multiple(() =>
    {
      Assert.That(result, Is.Not.Null);
      Assert.That(result!.Data, Is.Null);
      Assert.That(result.Errors, Is.Not.Null);
      Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));

      var errors = (result.Errors as IEnumerable<IdentityError>)!.Select(e => e.Code).Distinct();
      Assert.That(errors, Is.EquivalentTo(errorFields));
    });
  }

  [Test]
  [Order(3)]
  [TestCaseSource(nameof(_credential))]
  public async Task AuthenticateTest(string credentialName, string password, HttpStatusCode statusCode)
  {
    var credential = new UserCredential()
    {
      UserName = credentialName,
      Password = password
    };

    var result = await _userService.Authenticate(credential);

    Assert.That(result.StatusCode, Is.EqualTo(statusCode));
  }

  [Test]
  [Order(4)]
  [TestCase("usertest1", "wrongpassword", "1234abc")]
  public async Task LockoutTest(string userName, string password, string righPassword)
  {
    var credential = new UserCredential()
    {
      UserName = userName,
      Password = password
    };

    for(var i = 0; i < 5; i++)
      await _userService.Authenticate(credential);

    credential.Password = righPassword;

    var result = await _userService.Authenticate(credential);

    Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.Forbidden));
  }
}