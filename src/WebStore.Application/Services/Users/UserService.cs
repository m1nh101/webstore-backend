using Microsoft.AspNetCore.Identity;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Common.Contracts;
using WebStore.Application.Common.Helpers;
using WebStore.Domain.Entities;

namespace WebStore.Application.Services.Users;

public class UserService : IUserService
{
  private readonly UserManager<User> _userManager;
  private readonly IJwtGenerator _jwt;

  public UserService(UserManager<User> userManager,
    IJwtGenerator jwt)
  {
    _userManager = userManager;
    _jwt = jwt;
  }

  public async Task<IResponse> AddNew(UserRegistrationCredential payload)
  {
    var user = new User
    {
      UserName = payload.UserName,
      Email = payload.Email
    };

    var createUserResult = await _userManager.CreateAsync(user, payload.Password);

    if(createUserResult.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, "customer");

      var data = await SucceedAuthentication(user);

      return ResponseFactory.Create(System.Net.HttpStatusCode.Created, data);
    }

    return ResponseFactory.Create(System.Net.HttpStatusCode.BadRequest, errors: createUserResult.Errors);
  }

  public async Task<IResponse> Authenticate(UserCredential credential)
  {
    var failedAuthenicationResponse = ResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized,
      errors: new { Message = "Sai email/tài khoản hoặc mật khẩu"});

    var user = await _userManager.FindByNameAsync(credential.UserName)
      ?? await _userManager.FindByEmailAsync(credential.UserName);

    if(user == null)
      return failedAuthenicationResponse;

    var checkPassword = await _userManager.CheckPasswordAsync(user, credential.Password);
    
    if(!checkPassword)
      return failedAuthenicationResponse;

    var data = await SucceedAuthentication(user);

    return ResponseFactory.Create(System.Net.HttpStatusCode.OK, data);
  }

  private async Task<UserAuthenticationResponse> SucceedAuthentication(User user)
  {
    var roles = await _userManager.GetRolesAsync(user);

    var jwtToken = _jwt.GenerateToken(user, roles);

    return new()
    {
      Id = user.Id,
      FullName = user.FullName,
      UserName = user.UserName!,
      Token = jwtToken
    };
  }
}
