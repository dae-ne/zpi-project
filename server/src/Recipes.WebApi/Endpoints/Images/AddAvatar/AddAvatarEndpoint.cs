using Recipes.Application.Images.Commands.AddAvatar;

namespace Recipes.WebApi.Endpoints.Images.AddAvatar;

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
        var avatarUrl = httpContext.GenerateAvatarUrl(fileName);
        return Results.Created(avatarUrl, null);
    }
}