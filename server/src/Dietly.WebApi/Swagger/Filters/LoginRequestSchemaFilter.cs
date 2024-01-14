using Microsoft.AspNetCore.Identity.Data;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dietly.WebApi.Swagger.Filters;

public class LoginRequestSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type != typeof(LoginRequest))
        {
            return;
        }

        schema.Properties.Remove("twoFactorCode");
        schema.Properties.Remove("twoFactorRecoveryCode");
    }
}
