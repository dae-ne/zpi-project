using Dietly.Application.Images.Queries.GetFoodImage;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;

namespace Dietly.WebApi.Resources.Images.Food.Get;

public sealed class FoodImageGetEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/images/food/{fileName}")
        .WithTags("Images")
        .WithName("getFoodImage")
        .AllowAnonymous()
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");

    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetFoodImageQuery(fileName);
        var result = await mediator.Send(query);
        return result.ToHttpResult();
    }
}
