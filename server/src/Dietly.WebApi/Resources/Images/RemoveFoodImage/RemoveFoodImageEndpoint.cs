using Dietly.Application.Images.Commands.RemoveFoodImage;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Images.RemoveFoodImage;

[ApiEndpointDelete("/images/food/{fileName}")]
public sealed class RemoveFoodImageEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("removeFoodImage");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveFoodImageCommand(fileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
