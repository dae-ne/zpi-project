using Dietly.Application.Meals.Queries.GetMeal;

namespace Dietly.WebApi.Resources.Meals.GetMeal;

[ApiEndpointGet("/api/meals/{mealId}")]
public sealed class GetMealEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("getMeal")
        .Produces<GetMealResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var query = new GetMealQuery(mealId, userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetMealMapper.ToDto);
    }
}
