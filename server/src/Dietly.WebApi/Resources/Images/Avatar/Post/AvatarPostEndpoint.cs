using Dietly.Application.Images.Commands.AddAvatar;

namespace Dietly.WebApi.Resources.Images.Avatar.Post;

[ApiEndpointPost("/images/avatar")]
public sealed class AvatarPostEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("addAvatar")
        .DisableAntiforgery()
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(IFormFile file, HttpContext httpContext)
    {
        var command = new AddAvatarCommand(file.ToByteArray(), file.FileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateAvatarUrl);
    }
}
