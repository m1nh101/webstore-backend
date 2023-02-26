namespace WebStore.Application.Services.Users;

public class UserAuthenticationResponse
{
  public string FullName { get; set; } = string.Empty;
  public string Token { get; set; } = string.Empty;
  public string Id { get; set; } = string.Empty;
  public string UserName { get; set; } = string.Empty;
}