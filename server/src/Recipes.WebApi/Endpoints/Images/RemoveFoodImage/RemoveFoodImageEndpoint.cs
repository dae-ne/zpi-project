using Recipes.Application.Images.Commands.RemoveFoodImage;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Images.RemoveFoodImage;

[ApiEndpointDelete("/images/food/{fileName}")]
public sealed class RemoveFoodImageEndpoint(IMediator mediator) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("removeFoodImage");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveFoodImageCommand(fileName);
        await mediator.Send(command);
        return Results.Ok();
    }
}
