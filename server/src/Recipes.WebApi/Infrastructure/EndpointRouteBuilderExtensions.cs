using System.Linq.Expressions;
using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace Recipes.WebApi.Infrastructure;

internal static class EndpointRouteBuilderExtensions
{
    public static RouteHandlerBuilder RegisterSingleEndpoint(this IEndpointRouteBuilder app,
        Type endpoint,
        IServiceProvider serviceProvider,
        out object instance)
    {
        var methods = endpoint.GetMethods()
            .Where(m => m.GetCustomAttributes<ApiEndpointHandlerAttribute>().Any())
            .ToList();

        if (!methods.Any())
        {
            throw new InvalidOperationException(
                $"Unable to find any method with the '{nameof(ApiEndpointHandlerAttribute)}' attribute in the '{endpoint.Name}' type.");
        }

        if (methods.Count > 1)
        {
            throw new InvalidOperationException(
                $"Found more than one method with the '{nameof(ApiEndpointHandlerAttribute)}' attribute in the '{endpoint.Name}' type.");
        }

        var method = methods.Single();

        if (method.ReturnType != typeof(Task<IResult>))
        {
            throw new InvalidOperationException(
                $"The '{method.Name}' method in the '{endpoint.Name}' type must return a '{typeof(Task<IResult>).Name}' type.");
        }

        var constructorParams = endpoint
            .GetConstructors().Single()
            .GetParameters()
            .Select(parameter => serviceProvider.GetRequiredService(parameter.ParameterType))
            .ToArray();

        instance = Activator.CreateInstance(endpoint, constructorParams)!;

        var route = endpoint.GetCustomAttribute<ApiEndpointAttribute>()!.Route;
        var pattern = $"{route}";

        var apiAttributeType = endpoint.GetCustomAttributes()
            .Where(a => a.GetType().IsSubclassOf(typeof(ApiEndpointAttribute)))
            .Select(a => a.GetType())
            .Distinct()
            .Single();

        var handlerDelegate = method.CreateDelegate(instance);

        var builder = apiAttributeType switch
        {
            _ when apiAttributeType == typeof(ApiEndpointGetAttribute) =>
                app.MapGet(pattern, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointPostAttribute) =>
                app.MapPost(pattern, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointPutAttribute) =>
                app.MapPut(pattern, handlerDelegate),
            _ when apiAttributeType == typeof(ApiEndpointDeleteAttribute) =>
                app.MapDelete(pattern, handlerDelegate),
            // TODO: exception type and message
            _ => throw new InvalidOperationException(
                $"The '{apiAttributeType.Name}' attribute is not supported.")
        };

        return builder
            .RequireAuthorization()
            .RequireCors();
    }

    private static Delegate CreateDelegate(this MethodInfo method, object instance)
    {
        return method.CreateDelegate(Expression.GetDelegateType(
                method.GetParameters()
                    .Select(parameter => parameter.ParameterType)
                    .Concat(new[] { method.ReturnType })
                    .ToArray()),
            instance);
    }
}
