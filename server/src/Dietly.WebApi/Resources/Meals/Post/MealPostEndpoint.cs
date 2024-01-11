using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Meals.Post.Models;

namespace Dietly.WebApi.Resources.Meals.Post;

public sealed class MealPostEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Post("/api/meals")
        .WithTags("Meals")
        .WithName("addMeal")
        .Produces(201);

    public async Task<IResult> HandleAsync(MealPostRequest request, HttpContext httpContext) // TODO: HttpRequest?
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);

        return result.Match(
            id => Results.Created(httpContext.Request.GenerateUrlForCreatedItem(id), null),
            HandleError);
    }
}
