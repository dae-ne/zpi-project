using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Recipes.WebApi.Swagger;

internal static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSwaggerGenerator(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Recipes API",
                Version = "v1"
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            options.DocInclusionPredicate(AccountEndpointsHelper.IsEndpointValid);
            options.CustomOperationIds(OperationIdHelper.GetOperationId);
            // options.OperationFilter<ExcludeTwoFactorAuthPropertiesFilter>();
            options.OperationFilter<AddLocationHeaderToPostResponseFilter>();
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
