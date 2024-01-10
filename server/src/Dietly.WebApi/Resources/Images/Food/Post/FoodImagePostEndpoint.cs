using Dietly.Application.Images.Commands.AddFoodImage;

namespace Dietly.WebApi.Resources.Images.Food.Post;

[ApiEndpointPost("/images/food")]
public sealed class FoodImagePostEndpoint(IMediator mediator) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Images")
        .WithName("addFoodImage")
        .DisableAntiforgery()
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(IFormFile file, HttpContext httpContext)
    {
        var command = new AddFoodImageCommand(file.ToByteArray(), file.FileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateFoodImageUrl);
    }
}
