namespace Dietly.WebApi.Resources.Meals.UpdateMeal;

[ApiEndpointPut("/api/meals/{mealId}")]
public sealed class UpdateMealEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("updateMeal")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId, UpdateMealRequest request)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(mealId, userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
