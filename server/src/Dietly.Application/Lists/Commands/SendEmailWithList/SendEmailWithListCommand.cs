using Dietly.Application.Common.Results;

namespace Dietly.Application.Lists.Commands.SendEmailWithList;

public sealed class SendEmailWithListCommand : IRequest<Result<Unit>>
{
    public int UserId { get; init; }

    public IEnumerable<int> IngredientIds { get; init; } = Enumerable.Empty<int>();
}

[UsedImplicitly]
internal sealed class SendEmailWithListCommandHandler(
    IAppDbContext db,
    IUserService userService,
    IEmailService emailService)
    : IRequestHandler<SendEmailWithListCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(SendEmailWithListCommand request, CancellationToken cancellationToken)
    {
        if (!request.IngredientIds.Any())
        {
            return Errors.Invalid("Ingredients list is empty");
        }

        var user = await userService.GetUserAsync(request.UserId, cancellationToken);
        var ingredients = await db.Ingredients
            .AsNoTracking()
            .Where(i => request.IngredientIds.Contains(i.Id))
            .ToListAsync(cancellationToken);

        var message = string.Join("\n", ingredients);
        await emailService.SendAsync(user.Email, "Shopping list", message);
        return Unit.Value;
    }
}
