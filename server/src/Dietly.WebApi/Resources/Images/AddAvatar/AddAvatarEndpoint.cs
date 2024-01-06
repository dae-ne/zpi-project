using Dietly.Application.Images.Commands.AddAvatar;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Images.AddAvatar;

[ApiEndpointPost("/images/avatar")]
public sealed class AddAvatarEndpoint(IMediator mediator) : IConfigurableApiEndpoint
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
        var fileName = await mediator.Send(command);
        var avatarUrl = httpContext.Request.GenerateAvatarUrl(fileName);
        return Results.Created(avatarUrl, null);
    }
}