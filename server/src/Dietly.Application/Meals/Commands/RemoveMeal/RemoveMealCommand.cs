using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Events.Meal;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Commands.RemoveMeal;

public sealed record RemoveMealCommand(int MealId, int UserId) : IRequest<Result<object?>>;

[UsedImplicitly]
internal sealed class RemoveMealCommandHandler(IAppDbContext db) : IRequestHandler<RemoveMealCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Id == request.MealId)
            .ToListAsync(cancellationToken);

        if (meals.Count == 0)
        {
            return Results.NotFound("Meal not found");
        }

        var meal = meals.First();

        if (meal.Recipe.UserId != request.UserId)
        {
            return Results.Forbidden("Meal does not belong to user");
        }

        meal.AddDomainEvent(new MealRemovedEvent(meal));

        db.Meals.Remove(meal);
        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Results.Ok()
            : Results.UnknownError();
    }
}
