using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Meals.Put.Models;

namespace Dietly.WebApi.Resources.Meals.Put;

public sealed class MealPutEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Put("/api/meals/{mealId}")
        .WithTags("Meals")
        .WithName("updateMeal")
        .Produces(200);

    public async Task<IResult> HandleAsync(int mealId, MealPutRequest request)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(mealId, userId);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
