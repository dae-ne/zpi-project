using Recipes.Application.Images.Queries.GetAvatar;

namespace Recipes.WebApi.Endpoints.Images.GetAvatar;

[ApiEndpointGet("/images/avatar/{fileName}")]
public sealed class GetAvatarEndpoint(IMediator mediator) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("getAvatar")
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetAvatarQuery(fileName);
        var file = await mediator.Send(query);
        var fileExtension = fileName.Split('.').Last();
        var mimeType = fileExtension switch
        {
            "png" => "image/png",
            "jpg" => "image/jpeg",
            "jpeg" => "image/jpeg",
            "gif" => "image/gif",
            _ => "application/octet-stream"
        };
        return Results.File(file, mimeType);
    }
}