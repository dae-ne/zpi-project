using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.DependencyInjection;

namespace Dietly.WebApi.Infrastructure.Extensions;

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

    public static IServiceCollection AddHttpLogging(this IServiceCollection services, bool fullLogging = false)
    {
        if (fullLogging)
        {
            services.AddHttpLogging(_ => { });
            return services;
        }

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
