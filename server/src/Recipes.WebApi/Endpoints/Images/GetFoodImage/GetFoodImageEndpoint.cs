using Recipes.Application.Images.Queries.GetFoodImage;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Images.GetFoodImage;

[ApiEndpointGet("/images/food/{fileName}")]
public sealed class GetFoodImageEndpoint(IMediator mediator) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("getFoodImage")
        .AllowAnonymous()
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetFoodImageQuery(fileName);
        var image = await mediator.Send(query);
        var fileExtension = fileName.Split('.').Last();
        var mimeType = fileExtension switch
        {
            "png" => "image/png",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };
        return Results.File(image, mimeType);
    }
}
