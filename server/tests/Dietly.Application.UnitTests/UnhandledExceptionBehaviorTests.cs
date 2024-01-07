using Dietly.Application.Common.Behaviors;
using Dietly.Application.Common.Result;
using Dietly.Application.Users.Queries.GetUser;
using Dietly.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace Dietly.Application.UnitTests;

public class UnhandledExceptionBehaviorTests
{
    [Fact]
    public async Task Handle_ShouldReturnResultFromNextDelegate_WhenNoExceptionIsThrown()
    {
        // Arrange
        var logger = new Mock<ILogger<GetUserQuery>>();
        var behavior = new UnhandledExceptionBehavior<GetUserQuery, Result<User>>(logger.Object);
        var next = new Mock<RequestHandlerDelegate<Result<User>>>();
        next.Setup(x => x()).ReturnsAsync(Result<User>.Ok());

        // Act
        var result = await behavior.Handle(new GetUserQuery(1), next.Object, CancellationToken.None);

        // Assert
        Assert.Equal(ResultType.Ok, result.Type);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnknownErrorResult_WhenExceptionIsThrown()
    {
        // Arrange
        var logger = new Mock<ILogger<GetUserQuery>>();
        var behavior = new UnhandledExceptionBehavior<GetUserQuery, Result<User>>(logger.Object);
        var next = new Mock<RequestHandlerDelegate<Result<User>>>();
        next.Setup(x => x()).ThrowsAsync(new Exception());

        // Act
        var result = await behavior.Handle(new GetUserQuery(1), next.Object, CancellationToken.None);

        // Assert
        Assert.Equal(ResultType.UnknownError, result.Type);
    }
}
