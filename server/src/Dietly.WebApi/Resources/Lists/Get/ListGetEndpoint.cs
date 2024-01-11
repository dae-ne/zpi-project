using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Lists.Get.Models;

namespace Dietly.WebApi.Resources.Lists.Get;

public sealed class ListGetEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/api/lists")
        .WithTags("Lists")
        .WithName("getList")
        .Produces<ListGetResponse>();

    public async Task<IResult> HandleAsync([AsParameters] ListGetQueryString queryString)
    {
        var userId = currentUser.GetId();
        var query = queryString.ToQuery(userId);
        var result = await mediator.Send(query);
        return result.Match(list => Results.Ok(list.ToDto()), HandleError);
    }
}
