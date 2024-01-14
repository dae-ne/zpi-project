using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Resources.Lists.Post.Models;

namespace Dietly.WebApi.Resources.Lists.Post;

public class SendEmailWithListEndpoint(IMediator mediator, CurrentUser currentUser) : ApiEndpointBase
{
    public override void Configure(IApiEndpointBuilder builder) => builder
        .Post("/api/lists/sendEmail")
        .WithTags("Lists")
        .WithName("sendEmailWithList")
        .Produces(200);

    public async Task<IResult> HandleAsync(SendEmailWithListRequest request)
    {
        var userId = currentUser.GetId();
        var command = request.ToCommand(userId);
        var result = await mediator.Send(command);
        return result.Match(Results.Ok, HandleError);
    }
}
