using Dietly.WebApi.Swagger.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Dietly.WebApi.Swagger;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Dietly API",
                Version = "v1"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.DocInclusionPredicate(SwaggerEndpointsConfigHelper.IsEndpointValid);
            options.CustomOperationIds(SwaggerEndpointsConfigHelper.GetOperationId);
            options.OperationFilter<LoginOperationFilter>();
            options.OperationFilter<LocationHeaderOperationFilter>();
            options.SchemaFilter<LoginRequestSchemaFilter>();
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = "Bearer",
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }
}
