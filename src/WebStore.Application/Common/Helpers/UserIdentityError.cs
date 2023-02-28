using Microsoft.AspNetCore.Identity;

namespace WebStore.Application.Common.Helpers;

public class UserIdentityError : IdentityErrorDescriber
{
  public override IdentityError InvalidEmail(string? email)
  {
    return new()
    {
      Description = $"{email} không đúng định dạng",
      Code = "email"
    };
  }

  public override IdentityError PasswordTooShort(int length)
  {
    return new()
    {
      Description = "Mật khẩu phải có độ dài tối thiểu 6 ký tự",
      Code = "password"
    };
  }
  public override IdentityError PasswordRequiresDigit()
  {
    return new()
    {
      Description = "Mật khẩu phải bao gồm tối thiểu 1 chữ số",
      Code = "password"
    };
  }

  public override IdentityError InvalidUserName(string? userName)
  {
    return new()
    {
      Description = $"{userName} không hợp lệ",
      Code = "username"
    };
  }

  public override IdentityError DuplicateEmail(string email)
  {
    return new IdentityError
    {
      Description = $"{email} đã được sử dụng",
      Code = "email"
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