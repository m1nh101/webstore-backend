using Microsoft.AspNetCore.Identity;

namespace WebStore.Application.Common.Helpers;

public class UserIdentityError : IdentityErrorDescriber
{
  public override IdentityError DuplicateEmail(string email)
  {
    return new IdentityError
    {
      Description = $"{email} đã được sử dụng",
      Code = "Email"
    };
  }

  public override IdentityError DuplicateUserName(string userName)
  {
    return new IdentityError
    {
      Description = $"{userName} đã được sử dụng",
      Code = "username"
    };
  }
}