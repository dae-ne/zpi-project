using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
using Dietly.WebApi.Resources.Plans.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get;

public sealed class PlansGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/plans")
        .WithTags("Plans")
        .WithName("getPlans")
        .Produces<PlansGetResponse>();

    public async Task<IResult> HandleAsync([AsParameters] PlansGetQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
