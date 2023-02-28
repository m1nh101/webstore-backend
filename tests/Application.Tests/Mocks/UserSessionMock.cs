using Moq;
using WebStore.Application.Common.Abstractions;

namespace Application.Tests.Mocks;

public static class UserSessionMock
{
  public static Mock<IUserSession> MockUserSession()
  {
    var mock = new Mock<IUserSession>();

    mock.Setup(e => e.UserId).Returns("aaaa-bbbb-cccc-dddd");

    return mock;
  }
}
