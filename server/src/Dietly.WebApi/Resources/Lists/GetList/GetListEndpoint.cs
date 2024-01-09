using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Lists.GetList;

[ApiEndpointGet("/api/lists")]
public sealed class GetListEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Lists")
        .WithName("getList")
        .Produces<GetListResponse>(200, "application/json");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync([AsParameters] GetListQueryString queryString)
    {
        var userId = currentUser.GetId();
        var query = queryString.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.ToHttpResult(GetListMapper.ToDto);
    }
}
