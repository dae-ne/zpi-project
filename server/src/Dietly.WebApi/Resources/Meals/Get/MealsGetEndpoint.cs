using Dietly.WebApi.Resources.Meals.Get.Models;

namespace Dietly.WebApi.Resources.Meals.Get;

[ApiEndpointGet("/api/meals")]
public sealed class MealsGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Meals")
        .WithName("getMeals")
        .Produces<MealsGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] MealsGetQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(MealsGetMapper.ToDto);
    }
}
