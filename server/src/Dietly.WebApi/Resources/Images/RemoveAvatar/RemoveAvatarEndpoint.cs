using Dietly.Application.Images.Commands.RemoveAvatar;

namespace Dietly.WebApi.Resources.Images.RemoveAvatar;

[ApiEndpointDelete("/images/avatar/{fileName}")]
public sealed class RemoveAvatarEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("removeAvatar")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveAvatarCommand(fileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
