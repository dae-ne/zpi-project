using Dietly.Application.Meals.Commands.RemoveMeal;
using Dietly.WebApi.Infrastructure.ApiEndpoints;

namespace Dietly.WebApi.Resources.Meals.Delete;

public sealed class MealDeleteEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Delete("/api/meals/{mealId}")
        .WithTags("Meals")
        .WithName("removeMeal")
        .Produces(200);

    public async Task<IResult> HandleAsync(int mealId)
    {
        var userId = currentUser.GetId();
        var command = new RemoveMealCommand(MealId: mealId, UserId: userId);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
