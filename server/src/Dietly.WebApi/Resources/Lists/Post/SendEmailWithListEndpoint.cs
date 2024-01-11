using Dietly.WebApi.Infrastructure.ApiEndpoints;
using Dietly.WebApi.Infrastructure.Extensions;
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

        if (userId != request.UserId)
        {
            return Results.Problem(
                statusCode: 400,
                detail: "The user ID provided in the URL does not match the one provided in the request body");
        }

        var command = request.ToCommand();
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
