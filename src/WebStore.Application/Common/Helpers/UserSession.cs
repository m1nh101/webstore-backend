using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WebStore.Application.Common.Abstractions;

namespace WebStore.Application.Common.Helpers;

public class UserSession : IUserSession
{
  private readonly IHttpContextAccessor _http;

  public UserSession(IHttpContextAccessor http)
  {
    _http = http;
  }

  public string UserId => _http.HttpContext!.User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
}
