using WebStore.Domain.Entities;

namespace WebStore.Application.Common.Abstractions;

public interface IJwtGenerator
{
  string GenerateToken(User user, IEnumerable<string> roles);
}