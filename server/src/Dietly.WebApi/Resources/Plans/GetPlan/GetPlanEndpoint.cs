using Dietly.Application.Plans.Queries.GetPlan;

namespace Dietly.WebApi.Resources.Plans.GetPlan;

[ApiEndpointGet("/api/plans/{day}")]
public sealed class GetPlanEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Plans")
        .WithName("getPlan")
        .Produces<GetPlanResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(string day)
    {
        var userId = currentUser.GetId();
        var query = new GetPlanQuery(DateOnly.Parse(day), userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetPlanMapper.ToDto);
    }
}
