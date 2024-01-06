using System.Security.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Moq;
using Dietly.WebApi.Services;

namespace Dietly.WebApi.UnitTests.Services;

public class CurrentUserTests
{
    [Fact]
    public void GetId_WhenUserIsAuthenticated_ReturnsId()
    {
        // Arrange
        var userId = "123";
        var httpContext = new DefaultHttpContext
        {
            User = new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }))
        };

        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

        var currentUser = new CurrentUser(httpContextAccessorMock.Object);

        // Act
        var result = currentUser.GetId();

        // Assert
        Assert.Equal(int.Parse(userId), result);
    }

    [Fact]
    public void GetId_WhenUserIsNotAuthenticated_ThrowsAuthenticationException()
    {
        // Arrange
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        httpContextAccessorMock.Setup(x => x.HttpContext).Returns((HttpContext?)null);

        var currentUser = new CurrentUser(httpContextAccessorMock.Object);

        // Act & Assert
        Assert.Throws<AuthenticationException>(() => currentUser.GetId());
    }
}
