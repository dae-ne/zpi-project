using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;
using Recipes.Domain.Events.Meal;

namespace Recipes.Application.Meals.Commands.UpdateMeal;

public sealed class UpdateMealCommand : IRequest
{
    public int MealId { get; init; }

    public int UserId { get; init; }

    public DateTime Date { get; init; }

    public bool Completed { get; init; }
}

[UsedImplicitly]
internal sealed class UpdateMealCommandHandler(IAppDbContext db) : IRequestHandler<UpdateMealCommand>
{
    public async Task Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {

        var meal = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .SingleOrDefaultAsync(m => m.Id == request.MealId, cancellationToken);

        if (meal is null)
        {
            // TODO: handle not found
        }

        if (meal.Recipe.UserId != request.UserId)
        {
            // TODO: handle unauthorized
        }

        var oldMeal = (Meal)meal.Clone();

        meal.Date = request.Date;
        meal.Completed = request.Completed;

        meal.AddDomainEvent(new MealUpdatedEvent(oldMeal, meal));

        await db.SaveChangesAsync(cancellationToken);
    }
}
