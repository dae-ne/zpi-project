using Dietly.Application.Common.Results;
using Dietly.Domain.Events.Meal;

namespace Dietly.Application.Meals.Commands.RemoveMeal;

public sealed record RemoveMealCommand(int MealId, int UserId) : IRequest<Result<Unit>>;

[UsedImplicitly]
internal sealed class RemoveMealCommandHandler(IAppDbContext db) : IRequestHandler<RemoveMealCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Id == request.MealId)
            .ToListAsync(cancellationToken);

        if (meals.Count == 0)
        {
            return Errors.NotFound("Meal not found");
        }

        var meal = meals.First();

        if (meal.Recipe.UserId != request.UserId)
        {
            return Errors.Forbidden("Meal does not belong to user");
        }

        meal.AddDomainEvent(new MealRemovedEvent(meal));

        db.Meals.Remove(meal);
        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Unit.Value
            : Errors.Unknown();
    }
}
