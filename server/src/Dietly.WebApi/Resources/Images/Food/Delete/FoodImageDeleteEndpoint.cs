using Dietly.Application.Images.Commands.RemoveFoodImage;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;

namespace Dietly.WebApi.Resources.Images.Food.Delete;

public sealed class FoodImageDeleteEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Delete("/images/food/{fileName}")
        .WithTags("Images")
        .WithName("removeFoodImage")
        .Produces(200);

    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveFoodImageCommand(fileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
