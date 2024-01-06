using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.Meal;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Commands.UpdateMeal;

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

        // TODO: remove pragma
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (meal.Recipe.UserId != request.UserId)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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
