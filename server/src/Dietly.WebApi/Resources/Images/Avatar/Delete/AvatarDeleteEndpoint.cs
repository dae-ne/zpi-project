using Dietly.Application.Images.Commands.RemoveAvatar;
using Dietly.WebApi.Infrastructure.ApiEndpoints;

namespace Dietly.WebApi.Resources.Images.Avatar.Delete;

public sealed class AvatarDeleteEndpoint(IMediator mediator) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Delete("/images/avatar/{fileName}")
        .WithTags("Images")
        .WithName("removeAvatar")
        .Produces(200);

    public async Task<IResult> HandleAsync(string fileName)
    {
        var command = new RemoveAvatarCommand(fileName);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
