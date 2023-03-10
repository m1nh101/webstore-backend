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
      Email = payload.Email,
      FullName = payload.FullName
    };

    var createUserResult = await _userManager.CreateAsync(user, payload.Password);

    if(createUserResult.Succeeded)
    {
      await _userManager.AddToRoleAsync(user, "customer");

      var data = await SucceedAuthentication(user);

      return ResponseFactory.Create(System.Net.HttpStatusCode.Created, data);
    }

    return ResponseFactory.Create(errors: createUserResult.Errors);
  }

  public async Task<IResponse> Authenticate(UserCredential credential)
  {
    var failedAuthenicationResponse = ResponseFactory.Create(System.Net.HttpStatusCode.Unauthorized,
      errors: new { Message = "Sai email/tài khoản hoặc mật khẩu"});

    var user = await _userManager.FindByNameAsync(credential.UserName)
      ?? await _userManager.FindByEmailAsync(credential.UserName);

    if(user == null)
      return failedAuthenicationResponse;

    var checkLockout = await CheckLockOutUser(user);

    if(checkLockout)
      return ResponseFactory.Create(errors: new { Message = "Tài khoản tạm thời bị vô hiệu hóa" });

    var checkPassword = await _userManager.CheckPasswordAsync(user, credential.Password);
    
    if(!checkPassword)
    {
      await _userManager.AccessFailedAsync(user);

      return failedAuthenicationResponse;
    }

    await _userManager.ResetAccessFailedCountAsync(user);

    var data = await SucceedAuthentication(user);

    return ResponseFactory.Create(data);
  }

  private async Task<bool> CheckLockOutUser(User user)
  {
    var isLockout = await _userManager.IsLockedOutAsync(user);

    var lockoutEndTime = await _userManager.GetLockoutEndDateAsync(user);

    return isLockout && lockoutEndTime > DateTime.Now;
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
