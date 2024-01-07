using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Plans.GetPlans;

[ApiEndpointGet("/api/plans")]
public sealed class GetPlansEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Plans")
        .WithName("getPlans")
        .Produces<GetPlansResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] GetPlansQueryParams queryParams)
    {
        var userId = currentUser.GetId();
        var query = queryParams.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetPlansMapper.ToDto);
    }
}
