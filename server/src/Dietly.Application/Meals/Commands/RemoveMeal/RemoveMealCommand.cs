using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.Meal;

namespace Dietly.Application.Meals.Commands.RemoveMeal;

public sealed record RemoveMealCommand(int MealId) : IRequest;

[UsedImplicitly]
internal sealed class RemoveMealCommandHandler(IAppDbContext db) : IRequestHandler<RemoveMealCommand>
{
    public async Task Handle(RemoveMealCommand request, CancellationToken cancellationToken)
    {
        // TODO: check user id
        var meal = await db.Meals
            .FindAsync(new object[] { request.MealId }, cancellationToken);

        if (meal is null)
        {
            // TODO: handle not found
        }

        // TODO: remove pragma
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        meal.AddDomainEvent(new MealRemovedEvent(meal));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

        db.Meals.Remove(meal);
        await db.SaveChangesAsync(cancellationToken);
    }
}
