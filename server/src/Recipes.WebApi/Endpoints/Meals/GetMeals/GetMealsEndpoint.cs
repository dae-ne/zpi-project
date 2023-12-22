using Recipes.Application.Meals.Queries.GetMeals;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Meals.GetMeals;

[ApiEndpointGet("/api/meals")]
public sealed class GetMealsEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("getMeals")
        .Produces<GetMealsResponse>(200, "application/json");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetMealsQuery(userId);
        var meals = await mediator.Send(query);
        var dto = meals.ToDto();
        return Results.Ok(dto);
    }
}
