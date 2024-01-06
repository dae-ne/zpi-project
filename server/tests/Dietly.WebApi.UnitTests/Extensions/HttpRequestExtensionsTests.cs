using Microsoft.AspNetCore.Http;
using Dietly.WebApi.Extensions;

namespace Dietly.WebApi.UnitTests.Extensions;

public class HttpRequestExtensionsTests
{
    [Fact]
    public void GenerateAvatarUrl_ShouldGenerateCorrectUrl()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "https",
                Host = new HostString("example.com")
            }
        };
        var request = context.Request;
        const string fileName = "avatar.png";
        const string expectedUrl = "https://example.com/images/avatar/avatar.png";

        // Act
        var generatedUrl = request.GenerateAvatarUrl(fileName);

        // Assert
        Assert.Equal(expectedUrl, generatedUrl);
    }

    [Fact]
    public void GenerateFoodImageUrl_ShouldGenerateCorrectUrl()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "https",
                Host = new HostString("example.com")
            }
        };
        var request = context.Request;
        const string fileName = "food.png";
        const string expectedUrl = "https://example.com/images/food/food.png";

        // Act
        var generatedUrl = request.GenerateFoodImageUrl(fileName);

        // Assert
        Assert.Equal(expectedUrl, generatedUrl);
    }

    [Fact]
    public void GenerateUrlForCreatedItem_ShouldGenerateCorrectUrl()
    {
        // Arrange
        var context = new DefaultHttpContext
        {
            Request =
            {
                Scheme = "https",
                Host = new HostString("example.com"),
                Path = "/api/items"
            }
        };
        var request = context.Request;
        const int id = 123;
        const string expectedUrl = "https://example.com/api/items/123";

        // Act
        var generatedUrl = request.GenerateUrlForCreatedItem(id);

        // Assert
        Assert.Equal(expectedUrl, generatedUrl);
    }
}
