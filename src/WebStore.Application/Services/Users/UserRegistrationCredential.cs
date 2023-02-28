namespace WebStore.Application.Services.Users;

public class UserRegistrationCredential : UserCredential
{
  public string FullName { get; set; } = string.Empty;
  public string Email { get; set; } = string.Empty;
}