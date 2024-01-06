using Dietly.Application.Plans.Queries.GetPlan;
using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Endpoints.Plans.GetPlan;

[ApiEndpointGet("/api/plans/{day}")]
public sealed class GetPlanEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
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
        var plan = await mediator.Send(query);
        var dto = plan.ToDto();
        return Results.Ok(dto);
    }
}
