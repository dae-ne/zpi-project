using Dietly.Application.Plans.Queries.GetPlan;
using Dietly.WebApi.Resources.Plans.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get;

[ApiEndpointGet("/api/plans/{day}")]
public sealed class PlanGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Plans")
        .WithName("getPlan")
        .Produces<PlanGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string day)
    {
        var userId = currentUser.GetId();
        var query = new GetPlanQuery(DateOnly.Parse(day), userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(PlansGetMapper.ToDto);
    }
}
