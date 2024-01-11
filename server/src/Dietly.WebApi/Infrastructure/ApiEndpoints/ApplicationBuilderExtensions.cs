using Dietly.WebApi.Infrastructure.Extensions;
using Microsoft.AspNetCore.Routing;

namespace Dietly.WebApi.Infrastructure.ApiEndpoints;

internal static class ApplicationBuilderExtensions
{
    public static IApplicationBuilder UseApiEndpoints(
        this IApplicationBuilder app)
        => app.UseApiEndpoints(_ => { });

    public static IApplicationBuilder UseApiEndpoints(
        this IApplicationBuilder app,
        Action<RouteHandlerBuilder> configure)
    {
        if (app is not IEndpointRouteBuilder routeBuilder)
        {
            throw new InvalidCastException($"Cannot cast '{nameof(app)}' to IEndpointRouteBuilder");
        }

        var endpoints = typeof(ApplicationBuilderExtensions).Assembly.ExportedTypes
            .Where(t => t.IsSubclassOf(typeof(ApiEndpointBase)))
            .ToList();

        foreach (var endpoint in endpoints)
        {
            configure(routeBuilder.RegisterSingleEndpoint(endpoint));
        }

        return app;
    }
}
