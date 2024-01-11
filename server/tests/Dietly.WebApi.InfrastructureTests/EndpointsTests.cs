using Microsoft.AspNetCore.Http;

namespace Dietly.WebApi.InfrastructureTests;

public class EndpointsTests
{
    [Fact]
    public void ShouldAllEndpointsHaveHandlers()
    {
        // Arrange
        var endpoints = GetEndpoints();

        // Act
        var endpointsWithoutHandlers = endpoints
            .Where(t => t.GetMethods().All(m =>
                !m.CustomAttributes.Any(a => a.GetType().IsSubclassOf(typeof(ApiEndpointHandlerAttribute)))))
            .ToList();

        // Assert
        Assert.Empty(endpointsWithoutHandlers);
    }

    [Fact]
    public void ShouldAllEndpointsReturnCorrectType()
    {
        // Arrange
        var endpoints = GetEndpoints();

        // Act
        var endpointsWithIncorrectReturnType = endpoints
            .Where(t => t.GetMethods().All(m =>
                m.CustomAttributes.Any(a => a.GetType().IsSubclassOf(typeof(ApiEndpointHandlerAttribute)))))
            .Where(t => t.GetMethods().Any(m => m.ReturnType != typeof(Task<IResult>)))
            .ToList();

        // Assert
        Assert.Empty(endpointsWithIncorrectReturnType);
    }

    [Fact]
    public void ShouldEveryEndpointHaveSingleHandler()
    {
        // Arrange
        var endpoints = GetEndpoints();

        // Act
        var endpointsWithMultipleHandlers = endpoints
            .Where(t => t.GetMethods().Count(m =>
                m.CustomAttributes.Any(a => a.GetType().IsSubclassOf(typeof(ApiEndpointHandlerAttribute)))) > 1)
            .ToList();

        // Assert
        Assert.Empty(endpointsWithMultipleHandlers);
    }

    [Fact]
    public void ShouldEveryEndpointImplementIConfigurableApiEndpoint()
    {
        // Arrange
        var endpoints = GetEndpoints();

        // Act
        var endpointsWithoutIConfigurableApiEndpoint = endpoints
            .Where(t => !t.IsAssignableTo(typeof(IApiEndpoint)))
            .ToList();

        // Assert
        Assert.Empty(endpointsWithoutIConfigurableApiEndpoint);
    }

    private static IList<Type> GetEndpoints()
    {
        var assembly = typeof(ApiEndpointAttribute).Assembly;
        var endpoints = assembly.GetTypes()
            .Where(x => x.CustomAttributes.Any(a => a.GetType().IsSubclassOf(typeof(ApiEndpointAttribute))))
            .ToList();

        return endpoints;
    }
}
