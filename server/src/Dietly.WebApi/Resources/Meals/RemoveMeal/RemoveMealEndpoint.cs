using Dietly.Application.Meals.Commands.RemoveMeal;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Meals.RemoveMeal;

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
