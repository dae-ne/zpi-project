using System.Reflection;
using Dietly.Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Dietly.WebApi.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseEndpoints(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
            builder.Run(async context =>
                await Results.Problem(statusCode: 500).ExecuteAsync(context)));

        app.RegisterEndpoints(app.Services, config =>
        {
            config.RequireAuthorization();
            config.RequireCors();
            config.Produces<ProblemDetails>(400);
            config.Produces<ProblemDetails>(404);
            config.Produces<ProblemDetails>(403);
            config.Produces<ProblemDetails>(500);
        });

        app.MapGroup("api/account")
            .MapIdentityApi<AppUser>()
            .WithTags("Account");
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
