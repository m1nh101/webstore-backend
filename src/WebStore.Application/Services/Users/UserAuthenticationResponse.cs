namespace WebStore.Application.Services.Users;

public class UserAuthenticationResponse
{
  public string FullName { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
  public string Id { get; set; } = string.Empty;
  public string UserName { get; set; } = string.Empty;
  public string Avatar { get; set; } = string.Empty;
}

public class UserData
{
  public UserData(string userName, string fullName, string avatar)
  {
    UserName = userName;
    FullName = fullName;
    Avatar = avatar;
  }

  public string UserName { get; private set; } = string.Empty;
  public string FullName { get; private set; } = string.Empty;
  public string Avatar { get; private set; } = string.Empty;
}