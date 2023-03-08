using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebStore.Application.Common.Abstractions;
using WebStore.Application.Services.Users;

namespace WebStore.Application.Common.Helpers;

public class UserSession : IUserSession, IExtractDataFromToken
{
  private readonly IHttpContextAccessor _http;

  public UserSession(IHttpContextAccessor http)
  {
    _http = http;
  }

  public string UserId => _http.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;

  public UserData ExtractInfo()
  {
    var user = _http.HttpContext!.User!;

    var userName = user.FindFirst(ClaimTypes.Name)!.Value;
    var fullName = user.FindFirst(ClaimTypes.GivenName)!.Value;
    
    return new(userName, fullName, string.Empty);
  }
}