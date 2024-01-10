using Dietly.WebApi.Resources.Lists.Post.Models;

namespace Dietly.WebApi.Resources.Lists.Post;

[ApiEndpointPost("/api/lists/sendEmail")]
public class SendEmailWithListEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Lists")
        .WithName("sendEmailWithList")
        .Produces(200);

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(SendEmailWithListRequest request)
    {
        var userId = currentUser.GetId();

        if (userId != request.UserId)
        {
            return Results.Problem(statusCode: 400, detail: "The user ID provided in the URL does not match the one provided in the request body");
        }

        var command = request.ToCommand();
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
