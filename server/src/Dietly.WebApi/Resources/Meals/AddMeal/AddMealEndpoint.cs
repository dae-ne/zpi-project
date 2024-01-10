namespace Dietly.WebApi.Resources.Meals.AddMeal;

[ApiEndpointPost("/api/meals")]
public sealed class AddMealEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
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
        var result = await mediator.Send(command);
        return result.ToHttpResult(httpContext.Request.GenerateUrlForCreatedItem);
    }
}
