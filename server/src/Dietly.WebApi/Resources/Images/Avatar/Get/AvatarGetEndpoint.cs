using Dietly.Application.Images.Queries.GetAvatar;

namespace Dietly.WebApi.Resources.Images.Avatar.Get;

[ApiEndpointGet("/images/avatar/{fileName}")]
public sealed class AvatarGetEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("getAvatar")
        .AllowAnonymous()
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetAvatarQuery(fileName);
        var result = await mediator.Send(query);
        return result.ToHttpResult();
    }
}
