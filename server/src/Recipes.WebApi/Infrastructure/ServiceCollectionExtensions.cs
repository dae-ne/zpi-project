using Microsoft.AspNetCore.HttpLogging;
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
                    .AllowAnyMethod()
                    .WithExposedHeaders("Location");
            });
        });

        return services;
    }

    public static IServiceCollection AddFullHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(_ => { });
        return services;
    }

    public static IServiceCollection AddBasicHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(options =>
        {
            options.LoggingFields =
                HttpLoggingFields.RequestProperties |
                HttpLoggingFields.RequestBody |
                HttpLoggingFields.ResponseBody;
        });

        return services;
    }
}
