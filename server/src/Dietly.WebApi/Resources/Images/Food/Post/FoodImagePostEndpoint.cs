using Dietly.Application.Images.Commands.AddFoodImage;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;

namespace Dietly.WebApi.Resources.Images.Food.Post;

public sealed class FoodImagePostEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Post("/images/food")
        .WithTags("Images")
        .WithName("addFoodImage")
        .DisableAntiforgery()
        .Produces(201);

    public async Task<IResult> HandleAsync(IFormFile file, HttpContext httpContext)
    {
        var command = new AddFoodImageCommand(file.ToByteArray(), file.FileName);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateFoodImageUrl);
    }
}
