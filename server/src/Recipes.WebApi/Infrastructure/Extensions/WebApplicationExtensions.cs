using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Recipes.Infrastructure.Identity;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Infrastructure.Extensions;

internal static class WebApplicationExtensions
{
    public static void UseEndpoints(this IEndpointRouteBuilder app, IServiceProvider serviceProvider)
    {
        app.RegisterEndpoints(serviceProvider);

        app.MapGroup("api/account")
            .MapIdentityApi<AppUser>()
            .WithTags("Account");
    }

    private static void RegisterEndpoints(this IEndpointRouteBuilder app, IServiceProvider serviceProvider)
    {
        var endpoints = typeof(WebApplicationExtensions).Assembly.ExportedTypes
            .Where(t => t.GetCustomAttributes<ApiEndpointGetAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointPostAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointPutAttribute>().Any() ||
                        t.GetCustomAttributes<ApiEndpointDeleteAttribute>().Any())
            .ToList();

        foreach (var endpoint in endpoints)
        {
            var builder = app.RegisterSingleEndpoint(endpoint, serviceProvider, out var instance);

            if (!typeof(IConfigurableApiEndpoint).IsAssignableFrom(endpoint))
            {
                continue;
            }

            var configureMethod = endpoint.GetMethod(nameof(IConfigurableApiEndpoint.Configure));
            configureMethod!.Invoke(instance, new object?[] { builder });
        }
    }
}