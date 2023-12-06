using System.Reflection;
using Microsoft.AspNetCore.Routing;
using Recipes.Infrastructure.Identity;

namespace Recipes.WebApi.Infrastructure;

internal static class WebApplicationExtensions
{
    public static void UseEndpoints(this WebApplication app, IServiceProvider serviceProvider)
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
        
        var numberOfApiAttributeTypes = endpoints
            .Select(t => t.GetCustomAttributes()
                .Where(a => a.GetType().IsSubclassOf(typeof(ApiEndpointAttribute)))
                .Select(a => a.GetType())
                .Distinct()
                .Count())
            .Distinct()
            .Count();

        if (numberOfApiAttributeTypes > 1)
        {
            // TODO: exception type and message
            throw new InvalidOperationException(
                $"Found more than one type of the '{nameof(ApiEndpointAttribute)}' attribute in the '{typeof(WebApplicationExtensions).Assembly.GetName().Name}' assembly.");
        }
                
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
