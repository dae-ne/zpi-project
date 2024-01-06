using Recipes.Application.Images.Commands.RemoveAvatar;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Images.RemoveAvatar;

[ApiEndpointDelete("/images/avatar/{fileName}")]
public sealed class RemoveAvatarEndpoint(IMediator mediator) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("removeAvatar");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveAvatarCommand(fileName);
        await mediator.Send(command);
        return Results.Ok();
    }
}
