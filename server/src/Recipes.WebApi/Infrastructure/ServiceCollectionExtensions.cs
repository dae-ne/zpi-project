using Microsoft.Extensions.DependencyInjection;

namespace Recipes.WebApi.Infrastructure;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCorsConfig(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });

        return services;
    }
}
