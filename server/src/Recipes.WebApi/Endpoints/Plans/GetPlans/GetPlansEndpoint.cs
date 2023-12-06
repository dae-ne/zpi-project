using Recipes.Application.Plans.Queries.GetPlans;

namespace Recipes.WebApi.Endpoints.Plans.GetPlans;

[ApiEndpointGet("/api/plans")]
public sealed class GetPlansEndpoint(IMediator mediator, CurrentUser currentUser) : IConfigurableApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Plans")
        .WithName("getPlans")
        .Produces<GetPlansResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync()
    {
        var userId = currentUser.GetId();
        var query = new GetPlansQuery { UserId = userId };
        var plans = await mediator.Send(query);
        var dto = plans.ToDto();
        return Results.Ok(dto);
    }
}
