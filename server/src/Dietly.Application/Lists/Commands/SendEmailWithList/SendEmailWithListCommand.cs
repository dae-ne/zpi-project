using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Lists.Commands.SendEmailWithList;

public sealed class SendEmailWithListCommand : IRequest<Result<object?>>
{
    public int UserId { get; init; }

    public DateTime From { get; init; }

    public DateTime To { get; init; }
}

[UsedImplicitly]
internal sealed class SendEmailWithListCommandHandler(IAppDbContext db, IUserService userService, IEmailService emailService)
    : IRequestHandler<SendEmailWithListCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(SendEmailWithListCommand request, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserAsync(request.UserId, cancellationToken);

        var meals = await db.Meals
            .Include(m => m.Recipe)
            .ThenInclude(r => r.Ingredients)
            .Where(m => m.Recipe.UserId == request.UserId)
            .Where(m => m.Date >= request.From && m.Date <= request.To)
            .ToListAsync(cancellationToken);

        var ingredients = meals
            .SelectMany(m => m.Recipe!.Ingredients)
            .GroupBy(i => i.Id)
            .Select(g => g.First())
            .ToList();

        var message = string.Join("\n", ingredients.Select(i => i.Name));

        await emailService.SendAsync(user.Email, "Shopping list", message);

        return Results.Ok();
    }
}
