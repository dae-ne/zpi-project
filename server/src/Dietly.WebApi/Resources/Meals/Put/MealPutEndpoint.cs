using Dietly.WebApi.Resources.Meals.Put.Models;

namespace Dietly.WebApi.Resources.Meals.Put;

[ApiEndpointPut("/api/meals/{mealId}")]
public sealed class MealPutEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("updateMeal")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId, MealPutRequest request)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(mealId, userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
