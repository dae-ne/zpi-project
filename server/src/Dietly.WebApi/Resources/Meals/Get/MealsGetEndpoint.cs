using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Meals.Get.Models;

namespace Dietly.WebApi.Resources.Meals.Get;

public sealed class MealsGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/meals")
        .WithTags("Meals")
        .WithName("getMeals")
        .Produces<MealsGetResponse>();

    public async Task<IResult> HandleAsync([AsParameters] MealsGetQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.Match(meals => Results.Ok(meals.ToDto()), HandleError);
    }
}
