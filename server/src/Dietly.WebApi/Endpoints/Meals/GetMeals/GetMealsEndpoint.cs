using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;
using Dietly.WebApi.Services;

namespace Dietly.WebApi.Endpoints.Meals.GetMeals;

[ApiEndpointGet("/api/meals")]
public sealed class GetMealsEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("getMeals")
        .Produces<GetMealsResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] GetMealsQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var meals = await mediator.Send(query);
        var dto = meals.ToDto();
        return Results.Ok(dto);
    }
}
