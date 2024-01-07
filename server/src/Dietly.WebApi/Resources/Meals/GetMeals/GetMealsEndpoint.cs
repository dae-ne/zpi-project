using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Meals.GetMeals;

[ApiEndpointGet("/api/meals")]
public sealed class GetMealsEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
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
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetMealsMapper.ToDto);
    }
}
