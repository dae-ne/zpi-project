using System.Linq.Expressions;
using System.Reflection;
using Dietly.WebApi.Infrastructure.Attributes;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Dietly.WebApi.Infrastructure.Extensions;

internal static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder RegisterSingleEndpoint(
        this IEndpointRouteBuilder app,
        Type endpoint,
        IServiceProvider services,
        out object instance)
    {
        var methods = endpoint.GetMethods()
            .Where(m => m.GetCustomAttributes<ApiEndpointHandlerAttribute>().Any())
            .ToList();

        switch (methods.Count)
        {
            case 0:
                throw new InvalidOperationException($"Unable to find any method with the '{nameof(ApiEndpointHandlerAttribute)}' attribute in the '{endpoint.Name}' type.");
            case > 1:
                throw new InvalidOperationException($"Found more than one method with the '{nameof(ApiEndpointHandlerAttribute)}' attribute in the '{endpoint.Name}' type.");
        }

        var method = methods.Single();

        if (method.ReturnType != typeof(Task<IResult>))
        {
            throw new InvalidOperationException($"The '{method.Name}' method in the '{endpoint.Name}' type must return a '{typeof(Task<IResult>).Name}' type.");
        }

        var constructorParams = endpoint
            .GetConstructors().Single()
            .GetParameters()
            .Select(parameter => services.CreateScope().ServiceProvider.GetRequiredService(parameter.ParameterType))
            .ToArray();

        instance = Activator.CreateInstance(endpoint, constructorParams)!;

        var route = endpoint.GetCustomAttribute<ApiEndpointAttribute>()!.Route;

        var apiAttributeType = endpoint.GetCustomAttributes()
            .Where(a => a.GetType().IsSubclassOf(typeof(ApiEndpointAttribute)))
            .Select(a => a.GetType())
            .Distinct()
            .Single();

        var handlerDelegate = method.CreateDelegate(instance);

        var builder = apiAttributeType switch
        {
            _ when apiAttributeType == typeof(ApiEndpointGetAttribute) =>
                app.MapGet(route, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointPostAttribute) =>
                app.MapPost(route, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointPutAttribute) =>
                app.MapPut(route, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointDeleteAttribute) =>
                app.MapDelete(route, handlerDelegate),
            // TODO: more http methods support
            _ => throw new InvalidOperationException($"The '{apiAttributeType.Name}' attribute is not supported.")
        };

        return builder
            .RequireAuthorization()
            .RequireCors();
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
