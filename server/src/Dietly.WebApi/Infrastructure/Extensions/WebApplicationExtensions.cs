using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Dietly.Infrastructure.Identity;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseEndpoints(this WebApplication app)
    {
        app.RegisterEndpoints(app.Services);

        app.MapGroup("api/account")
            .MapIdentityApi<AppUser>()
            .WithTags("Account");
    }

    private static void RegisterEndpoints(this IEndpointRouteBuilder app, IServiceProvider services)
    {
        var endpoints = typeof(WebApplicationExtensions).Assembly.ExportedTypes
            .Where(t => t.GetCustomAttributes<ApiEndpointGetAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointPostAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointPutAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointDeleteAttribute>().Any())
            .ToList();

        foreach (var endpoint in endpoints)
        {
            var builder = app.RegisterSingleEndpoint(endpoint, services, out var instance);

            if (!typeof(IConfigurableApiEndpoint).IsAssignableFrom(endpoint))
            {
                continue;
            }

            var configureMethod = endpoint.GetMethod(nameof(IConfigurableApiEndpoint.Configure));
            configureMethod!.Invoke(instance, new object?[] { builder });
        }
    }
}
