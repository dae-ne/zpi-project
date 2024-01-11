using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

internal static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder RegisterSingleEndpoint(this IEndpointRouteBuilder app, Type endpoint)
    {
        var methods = endpoint.GetMethods()
            .Where(m => m.IsPublic && m.Name.Contains("Handle"))
            .ToList();

        switch (methods.Count)
        {
            case 0: throw new InvalidOperationException($"The '{endpoint.Name}' endpoint has no handler method.");
            case > 1: throw new InvalidOperationException($"The '{endpoint.Name}' endpoint has more than one handler method.");
        }

        var constructorParams = endpoint
            .GetConstructors().Single()
            .GetParameters()
            .Select(parameter => app.ServiceProvider.CreateScope()
                .ServiceProvider.GetRequiredService(parameter.ParameterType))
            .ToArray();

        var instance = Activator.CreateInstance(endpoint, constructorParams)!;
        var configureMethod = endpoint.GetMethod(nameof(ApiEndpointBase.Configure))!;

        var fakeBuilder = new RouteHandlerBuilder(Enumerable.Empty<IEndpointConventionBuilder>());
        var endpointBuilder = new ApiEndpointBuilder(fakeBuilder, (ApiEndpointBase)instance);

        configureMethod.Invoke(instance, [endpointBuilder]);

        var route = ((ApiEndpointBase)instance).Route;
        var httpMethod = ((ApiEndpointBase)instance).HttpMethod;
        var method = methods.Single();

        var handlerDelegate = method.CreateDelegate(instance);

        var builder = httpMethod switch
        {
            _ when httpMethod == HttpMethod.Get => app.MapGet(route, handlerDelegate),
            _ when httpMethod == HttpMethod.Put => app.MapPut(route, handlerDelegate),
            _ when httpMethod == HttpMethod.Post => app.MapPost(route, handlerDelegate),
            _ when httpMethod == HttpMethod.Delete => app.MapDelete(route, handlerDelegate),
            _ when httpMethod == HttpMethod.Patch => app.MapPatch(route, handlerDelegate),
            _ => throw new InvalidOperationException($"The '{httpMethod}' http method is not supported.")
        };

        endpointBuilder = new ApiEndpointBuilder(builder, (ApiEndpointBase)instance);
        configureMethod.Invoke(instance, [endpointBuilder]);

        return builder;
    }

    private static Delegate CreateDelegate(this MethodInfo method, object instance)
    {
        return method.CreateDelegate(
            Expression.GetDelegateType(
                method.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .Concat(new[] { method.ReturnType })
                    .ToArray()),
            instance);
    }
}
