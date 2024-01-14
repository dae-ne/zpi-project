using Dietly.Application.Meals.Queries.GetMeal;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Meals.Get.Models;

namespace Dietly.WebApi.Resources.Meals.Get;

public sealed class MealGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/meals/{mealId}")
        .WithTags("Meals")
        .WithName("getMeal")
        .Produces<MealGetResponse>();

    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var query = new GetMealQuery(mealId, userId);
        var result = await mediator.Send(query);
        return result.Match(meal => Results.Ok(meal.ToDto()), HandleError);
    }
}
