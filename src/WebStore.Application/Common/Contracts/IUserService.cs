using WebStore.Application.Common.Abstractions;
using WebStore.Application.Services.Users;

namespace WebStore.Application.Common.Contracts;

public interface IUserService : IAddCommand<UserRegistrationCredential>
{
  Task<IResponse> Authenticate(UserCredential credential);
}