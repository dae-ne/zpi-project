using Dietly.Application.Common.Behaviors;
using Dietly.Application.Common.Results;
using Dietly.Application.Common.Results.ErrorDefinitions;
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
        next.Setup(x => x()).ReturnsAsync(new User());

        // Act
        var result = await behavior.Handle(new GetUserQuery(1), next.Object, CancellationToken.None);

        // Assert
        result.Match<object?>(
            _ => null,
            _ => throw new Exception("Should not be error"));
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
        result.Match<object?>(
            _ => throw new Exception("Should not be Ok"),
            error =>
            {
                Assert.IsType<UnknownError>(error);
                return null;
            });
    }

    [Fact]
    public async Task Handle_ShouldThrowException_WhenExceptionIsThrownAndResultIsNotResult()
    {
        // Arrange
        var logger = new Mock<ILogger<GetUserQuery>>();
        var behavior = new UnhandledExceptionBehavior<GetUserQuery, User>(logger.Object);
        var next = new Mock<RequestHandlerDelegate<User>>();
        next.Setup(x => x()).ThrowsAsync(new Exception());

        // Assert
        await Assert.ThrowsAsync<Exception>(() => behavior.Handle(new GetUserQuery(1), next.Object, CancellationToken.None));
    }
}
