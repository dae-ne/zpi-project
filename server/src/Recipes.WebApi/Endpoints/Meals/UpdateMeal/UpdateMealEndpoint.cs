using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Meals.UpdateMeal;

[ApiEndpointPut("/api/meals/{mealId}")]
public sealed class UpdateMealEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("updateMeal");
    
    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId, UpdateMealRequest request)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(mealId, userId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
