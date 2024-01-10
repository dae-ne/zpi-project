using Dietly.Application.Meals.Queries.GetMeal;
using Dietly.WebApi.Resources.Meals.Get.Models;
using MealsGetMapper = Dietly.WebApi.Resources.Meals.Get.Models.MealsGetMapper;

namespace Dietly.WebApi.Resources.Meals.Get;

[ApiEndpointGet("/api/meals/{mealId}")]
public sealed class MealGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("getMeal")
        .Produces<MealGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var query = new GetMealQuery(mealId, userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(MealsGetMapper.ToDto);
    }
}
