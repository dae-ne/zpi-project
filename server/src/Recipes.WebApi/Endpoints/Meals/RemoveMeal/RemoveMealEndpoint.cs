using Recipes.Application.Meals.Commands.RemoveMeal;
using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Meals.RemoveMeal;

[ApiEndpointDelete("/api/meals/{mealId}")]
public sealed class RemoveMealEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("removeMeal");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId(); // TODO: Use this to validate that the user owns the meal
        var command = new RemoveMealCommand(mealId);
        await mediator.Send(command);
        return Results.Ok();
    }
}
