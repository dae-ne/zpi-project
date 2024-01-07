using Dietly.Application.Meals.Commands.RemoveMeal;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Meals.RemoveMeal;

[ApiEndpointDelete("/api/meals/{mealId}")]
public sealed class RemoveMealEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("removeMeal");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var command = new RemoveMealCommand(MealId: mealId, UserId: userId);
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
