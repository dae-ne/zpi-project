using Dietly.Application.Meals.Queries.GetMeal;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Endpoints.Meals.GetMeal;

[ApiEndpointGet("/api/meals/{mealId}")]
public sealed class GetMealEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
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
        var meal = await mediator.Send(query);
        var dto = meal.ToDto();
        return Results.Ok(dto);
    }
}
