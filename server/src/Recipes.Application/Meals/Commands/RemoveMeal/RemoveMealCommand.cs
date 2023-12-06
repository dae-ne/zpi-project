using Recipes.Application.Common.Interfaces;
using Recipes.Domain.Events.Meal;

namespace Recipes.Application.Meals.Commands.RemoveMeal;

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
        
        meal.AddDomainEvent(new MealRemovedEvent(meal));
        
        db.Meals.Remove(meal);
        await db.SaveChangesAsync(cancellationToken);
    }
}
