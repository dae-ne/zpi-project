using Dietly.Application.Common.Results;
using MediatR;

namespace Dietly.Application.InfrastructureTests;

public class RequestsTests
{
    [Fact]
    public void AllRequestsHaveResponse()
    {
        var requestsWithoutResponse = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i == typeof(IRequest)))
            .ToList();

        Assert.Empty(requestsWithoutResponse);
    }

    [Fact]
    public void AllRequestsShouldReturnResultClass()
    {
        var allRequestsInterfaces = typeof(DependencyInjection).Assembly.GetTypes()
            .SelectMany(t => t.GetInterfaces())
            .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>))
            .ToList();

        var requestsWithResult = allRequestsInterfaces
            .Where(t => t.GetGenericArguments().Any(a => a.IsGenericType && a.GetGenericTypeDefinition() == typeof(Result<>)))
            .ToList();

        Assert.Equal(allRequestsInterfaces.Count, requestsWithResult.Count);
    }

    [Fact]
    public void EveryRequestShouldHaveHandler()
    {
        var allRequests = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)))
            .ToList();

        var allHandlers = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        var requestsWithoutHandler = allRequests
            .Where(r => !allHandlers.Any(h => h.GetInterfaces().Any(i => i.GetGenericArguments().Any(a => a == r))))
            .ToList();

        Assert.Empty(requestsWithoutHandler);
    }

    [Fact]
    public void AllRequestsShouldBePublic()
    {
        var allRequests = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequest<>)))
            .ToList();

        var publicRequests = allRequests.Where(r => r.IsPublic).ToList();

        Assert.Equal(allRequests.Count, publicRequests.Count);
    }
}
