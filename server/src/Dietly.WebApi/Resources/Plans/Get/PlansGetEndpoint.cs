using Dietly.WebApi.Resources.Plans.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get;

[ApiEndpointGet("/api/plans")]
public sealed class PlansGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Plans")
        .WithName("getPlans")
        .Produces<PlansGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] PlansGetQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
