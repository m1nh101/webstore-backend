using Moq;
using WebStore.Application.Common.Abstractions;
using WebStore.Domain.Entities;

namespace Application.Tests.Mocks;

public static class JwtGeneratorMock
{
  public static Mock<IJwtGenerator> MockJwtGenerator(string token)
  {
    var mock = new Mock<IJwtGenerator>();

    mock.Setup(e => e.GenerateToken(It.IsAny<User>(), It.IsAny<IEnumerable<string>>())).Returns(token);

    return mock;
  }
}
