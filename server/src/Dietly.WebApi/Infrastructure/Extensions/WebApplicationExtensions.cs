using System.Reflection;
using Microsoft.AspNetCore.Routing;

namespace Dietly.WebApi.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseApiEndpoints(this WebApplication app, Action<RouteHandlerBuilder> configure)
    {
        app.UseExceptionHandler(builder =>
            builder.Run(async context =>
                await Results.Problem(statusCode: 500).ExecuteAsync(context)));

        app.RegisterEndpoints(app.Services, configure);
    }

    public static void UseIdentityApi<TUser>(
        this WebApplication app,
        string prefix,
        Action<IEndpointConventionBuilder> configure)
        where TUser : class, new()
    {
        var builder = app.MapGroup(prefix)
            .MapIdentityApi<TUser>();

        configure(builder);
    }

    private static void RegisterEndpoints(
        this IEndpointRouteBuilder app,
        IServiceProvider services,
        Action<RouteHandlerBuilder> configure)
    {
        var endpoints = typeof(WebApplicationExtensions).Assembly.ExportedTypes
            .Where(t =>
                t.GetCustomAttributes<ApiEndpointGetAttribute>().Any() ||
                t.GetCustomAttributes<ApiEndpointPostAttribute>().Any() ||
                t.GetCustomAttributes<ApiEndpointPutAttribute>().Any() ||
                t.GetCustomAttributes<ApiEndpointDeleteAttribute>().Any())
            .ToList();

        foreach (var endpoint in endpoints)
        {
            var builder = app.RegisterSingleEndpoint(endpoint, services, out var instance);
            configure(builder);

            if (!typeof(IApiEndpoint).IsAssignableFrom(endpoint))
            {
                continue;
            }

            var configureMethod = endpoint.GetMethod(nameof(IApiEndpoint.Configure));
            configureMethod!.Invoke(instance, [builder]);
        }
    }
}
