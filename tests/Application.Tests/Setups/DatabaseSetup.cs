using Application.Tests.Mocks;
using Microsoft.EntityFrameworkCore;
using WebStore.Infrastructure.Database;

namespace Application.Tests.Setups;

public class DatabaseSetup
{
  private readonly DbContextOptions<WebStoreContext> _options = new DbContextOptionsBuilder<WebStoreContext>()
    .UseInMemoryDatabase("TestDatabase")
    .Options;

  public WebStoreContext GetContext()
  {
    var userSession = UserSessionMock.MockUserSession().Object;

    return new WebStoreContext(_options, userSession);
  }
}
