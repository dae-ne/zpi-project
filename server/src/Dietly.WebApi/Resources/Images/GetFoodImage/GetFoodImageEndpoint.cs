using Dietly.Application.Images.Queries.GetFoodImage;

namespace Dietly.WebApi.Resources.Images.GetFoodImage;

[ApiEndpointGet("/images/food/{fileName}")]
public sealed class GetFoodImageEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("getFoodImage")
        .AllowAnonymous()
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetFoodImageQuery(fileName);
        var result = await mediator.Send(query);
        return result.ToHttpResult();
    }
}
