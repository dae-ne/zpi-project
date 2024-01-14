using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dietly.WebApi.Swagger.Filters;

internal sealed class LoginOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.OperationId == "login")
        {
            operation.Parameters.Clear();
        }
    }
}
