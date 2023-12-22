using Recipes.WebApi.Infrastructure.Attributes;
using Recipes.WebApi.Infrastructure.Interfaces;

namespace Recipes.WebApi.Endpoints.Meals.AddMeal;

[ApiEndpointPost("/api/meals")]
public sealed class AddMealEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("addMeal")
        .Produces(201);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(AddMealRequest request, HttpContext httpContext) // TODO: HttpRequest?
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var mealId = await mediator.Send(command);
        var url = httpContext.Request.GenerateUrlForCreatedItem(mealId);
        return Results.Created(url, null);
    }
}
