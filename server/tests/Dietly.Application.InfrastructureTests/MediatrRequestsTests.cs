using Dietly.Application.Common.Result;
using MediatR;

namespace Dietly.Application.InfrastructureTests;

public class MediatrRequestsTests
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
}
