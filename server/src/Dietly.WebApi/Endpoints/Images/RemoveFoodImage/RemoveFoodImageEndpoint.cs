using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;
using Dietly.Application.Images.Commands.RemoveFoodImage;

namespace Dietly.WebApi.Endpoints.Images.RemoveFoodImage;

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
