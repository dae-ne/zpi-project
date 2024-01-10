using Dietly.Application.Meals.Commands.RemoveMeal;

namespace Dietly.WebApi.Resources.Meals.Delete;

[ApiEndpointDelete("/api/meals/{mealId}")]
public sealed class MealDeleteEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("removeMeal")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var command = new RemoveMealCommand(MealId: mealId, UserId: userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
