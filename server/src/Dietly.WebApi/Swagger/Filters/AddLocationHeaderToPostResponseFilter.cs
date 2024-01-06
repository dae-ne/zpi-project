using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Dietly.WebApi.Swagger.Filters;

internal sealed class AddLocationHeaderToPostResponseFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (!operation.Responses.TryGetValue("201", out var response))
        {
            return;
        }

        response.Headers.Add("location", new OpenApiHeader
        {
            Description = "The URL of the newly created resource",
            Schema = new OpenApiSchema
            {
                Type = "string",
                Format = "uri"
            }
        });
    }
}
