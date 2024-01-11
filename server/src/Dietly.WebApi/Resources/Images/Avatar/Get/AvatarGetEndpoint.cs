using Dietly.Application.Images.Queries.GetAvatar;
using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;

namespace Dietly.WebApi.Resources.Images.Avatar.Get;

public sealed class AvatarGetEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Get("/images/avatar/{fileName}")
        .WithTags("Images")
        .WithName("getAvatar")
        .AllowAnonymous()
        .Produces<byte[]>(200, "image/png", "image/jpeg", "image/gif", "application/octet-stream");

    public async Task<IResult> HandleAsync(string fileName)
    {
        var query = new GetAvatarQuery(fileName);
        var result = await mediator.Send(query);
        return result.ToHttpResult();
    }
}
