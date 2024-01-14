using Dietly.WebApi.Infrastructure.ApiEndpoints;

namespace Dietly.WebApi.InfrastructureTests;

public class EndpointsTests
{
    [Fact]
    public void ShouldEveryEndpointHaveOneHandler()
    {
        // Arrange
        var endpoints = GetEndpoints();

        // Act
        var notValidEndpoints = endpoints
            .Where(t => t.GetMethods().Count(m => m.IsPublic && m.Name.Contains("Handle")) != 1)
            .ToList();

        // Assert
        Assert.Empty(notValidEndpoints);
    }

    private static IList<Type> GetEndpoints()
    {
        var endpoints = typeof(ApiEndpointBase).Assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && t.IsSubclassOf(typeof(ApiEndpointBase)))
            .ToList();

        return endpoints;
    }
}
