using Dietly.Application.Images.Commands.RemoveFoodImage;

namespace Dietly.WebApi.Resources.Images.Food.Delete;

[ApiEndpointDelete("/images/food/{fileName}")]
public sealed class FoodImageDeleteEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("removeFoodImage")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveFoodImageCommand(fileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}