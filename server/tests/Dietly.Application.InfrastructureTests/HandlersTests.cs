using MediatR;

namespace Dietly.Application.InfrastructureTests;

public class HandlersTests
{
    [Fact]
    public void AllHandlersShouldBeInternal()
    {
        var allHandlers = typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRequestHandler<,>)))
            .ToList();

        var publicHandlers = allHandlers.Where(h => h.IsPublic).ToList();

        Assert.Empty(publicHandlers);
    }
}
