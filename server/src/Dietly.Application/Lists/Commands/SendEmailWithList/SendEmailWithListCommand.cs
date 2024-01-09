using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Lists.Commands.SendEmailWithList;

public sealed class SendEmailWithListCommand : IRequest<Result<object?>>
{
    public int UserId { get; init; }

    public IEnumerable<string> Ingredients { get; init; } = Enumerable.Empty<string>();
}

[UsedImplicitly]
internal sealed class SendEmailWithListCommandHandler(IUserService userService, IEmailService emailService)
    : IRequestHandler<SendEmailWithListCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(SendEmailWithListCommand request, CancellationToken cancellationToken)
    {
        if (!request.Ingredients.Any())
        {
            return Results.ValidationError("Ingredients list is empty");
        }

        var user = await userService.GetUserAsync(request.UserId, cancellationToken);

        var message = string.Join("\n", request.Ingredients);

        await emailService.SendAsync(user.Email, "Shopping list", message);

        return Results.Ok();
    }
}
