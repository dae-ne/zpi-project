using Dietly.Application.Images.Commands.AddFoodImage;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Images.AddFoodImage;

[ApiEndpointPost("/images/food")]
public sealed class AddFoodImageEndpoint(IMediator mediator) : IConfigurableApiEndpoint
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
        var fileName = await mediator.Send(command);
        var imageUrl = httpContext.Request.GenerateFoodImageUrl(fileName);
        return Results.Created(imageUrl, null);
    }
}