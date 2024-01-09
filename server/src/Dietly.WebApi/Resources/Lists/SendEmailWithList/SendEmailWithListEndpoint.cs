using Dietly.WebApi.Infrastructure.Attributes;
using Dietly.WebApi.Infrastructure.Interfaces;

namespace Dietly.WebApi.Resources.Lists.SendEmailWithList;

[ApiEndpointPost("/api/lists/send-email")]
public class SendEmailWithListEndpoint(IMediator mediator, CurrentUser currentUser) : IApiEndpoint
{
    public void Configure(RouteHandlerBuilder builder) => builder
        .WithTags("Lists")
        .WithName("sendEmailWithList");

    [ApiEndpointHandler]
    public async Task<IResult> HandleAsync(SendEmailWithListRequest request)
    {
        var userId = currentUser.GetId();

        if (userId != request.UserId)
        {
            return Results.Forbid();
        }

        var command = request.ToCommand();
        var result = await mediator.Send(command);
        return result.ToHttpResult();
    }
}
