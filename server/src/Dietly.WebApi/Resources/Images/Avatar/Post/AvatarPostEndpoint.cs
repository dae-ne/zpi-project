using Dietly.Application.Images.Commands.AddAvatar;
using Dietly.WebApi.Infrastructure.ApiEndpoints;

namespace Dietly.WebApi.Resources.Images.Avatar.Post;

public sealed class AvatarPostEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Post("/images/avatar")
        .WithTags("Images")
        .WithName("addAvatar")
        .DisableAntiforgery()
        .Produces(201);

    public async Task<IResult> HandleAsync(IFormFile file, HttpContext httpContext)
    {
        var command = new AddAvatarCommand(file.ToByteArray(), file.FileName);
        var result = await mediator.Send(command);

        return result.Match(
            fileName => Results.Created(httpContext.Request.GenerateAvatarUrl(fileName), null),
            HandleError);
    }
}
