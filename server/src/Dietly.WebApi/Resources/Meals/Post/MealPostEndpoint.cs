using Dietly.WebApi.Resources.Meals.Post.Models;

namespace Dietly.WebApi.Resources.Meals.Post;

[ApiEndpointPost("/api/meals")]
public sealed class MealPostEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("addMeal")
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(MealPostRequest request, HttpContext httpContext) // TODO: HttpRequest?
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateUrlForCreatedItem);
    }
}
