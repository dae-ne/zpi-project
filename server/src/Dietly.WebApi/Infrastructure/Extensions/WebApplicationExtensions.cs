using System.Reflection;
using Dietly.Infrastructure.Identity;
using Microsoft.AspNetCore.Routing;

namespace Dietly.WebApi.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseEndpoints(this WebApplication app)
    {
        app.UseExceptionHandler(builder =>
            builder.Run(async context =>
                await Results.Problem(statusCode: 400, detail: "abcd").ExecuteAsync(context)));

        app.RegisterEndpoints(app.Services);

        app.MapGroup("api/account")
            .MapIdentityApi<AppUser>()
            .WithTags("Account");
    }

    private static void RegisterEndpoints(this IEndpointRouteBuilder app, IServiceProvider services)
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

            if (!typeof(IApiEndpoint).IsAssignableFrom(endpoint))
            {
                continue;
            }

            var configureMethod = endpoint.GetMethod(nameof(IApiEndpoint.Configure));
            configureMethod!.Invoke(instance, [builder]);
        }
    }
}
