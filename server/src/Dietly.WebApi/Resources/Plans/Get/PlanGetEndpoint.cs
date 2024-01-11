using Dietly.Application.Plans.Queries.GetPlan;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Resources.Plans.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get;

public sealed class PlanGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/plans/{day}")
        .WithTags("Plans")
        .WithName("getPlan")
        .Produces<PlanGetResponse>();

    public async Task<IResult> HandleAsync(string day)
    {
        var userId = currentUser.GetId();
        var query = new GetPlanQuery(DateOnly.Parse(day), userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
