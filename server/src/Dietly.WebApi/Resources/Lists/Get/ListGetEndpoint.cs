using Dietly.WebApi.Resources.Lists.Get.Models;

namespace Dietly.WebApi.Resources.Lists.Get;

[ApiEndpointGet("/api/lists")]
public sealed class ListGetEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Lists")
        .WithName("getList")
        .Produces<ListGetResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] ListGetQueryString queryString)
    {
        var userId = currentUser.GetId();
        var query = queryString.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(Mapper.ToDto);
    }
}
