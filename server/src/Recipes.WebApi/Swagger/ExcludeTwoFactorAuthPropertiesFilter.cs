using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Recipes.WebApi.Swagger;

// TODO: it's not working
internal sealed class ExcludeTwoFactorAuthPropertiesFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId != "login")
        {
            return;
        }
        
        var twoFactorProperties = operation.RequestBody?.Content["application/json"]?.Schema.Properties
            .Where(p => p.Key.Contains("twoFactor"))
            .Select(p => p.Key)
            .ToList();
        
        if (twoFactorProperties is null)
        {
            return;
        }
        
        foreach (var property in twoFactorProperties)
        {
            operation.RequestBody?.Content["application/json"]?.Schema.Properties.Remove(property);
        }
    }
}
