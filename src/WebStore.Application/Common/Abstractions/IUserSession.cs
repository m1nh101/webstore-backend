namespace WebStore.Application.Common.Abstractions;

public interface IUserSession
{
  public string UserId { get; }
}

public class UserSession : IUserSession
{
  public string UserId => throw new NotImplementedException();
}