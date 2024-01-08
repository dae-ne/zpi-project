using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Events.Meal;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Commands.UpdateMeal;

public sealed class UpdateMealCommand : IRequest<Result<object?>>
{
    public int MealId { get; init; }

    public int UserId { get; init; }

    public DateTime Date { get; init; }

    public bool Completed { get; init; }
}

[UsedImplicitly]
internal sealed class UpdateMealCommandHandler(IAppDbContext db) : IRequestHandler<UpdateMealCommand, Result<object?>>
{
    public async Task<Result<object?>> Handle(UpdateMealCommand request, CancellationToken cancellationToken)
    {
        var meal = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .SingleOrDefaultAsync(m => m.Id == request.MealId, cancellationToken);

        if (meal is null)
        {
            return Results.NotFound("Meal not found");
        }

        if (meal.Recipe.UserId != request.UserId)
        {
            return Results.Forbidden("Meal does not belong to user");
        }

        var oldMeal = (Meal)meal.Clone();

        meal.Date = request.Date;
        meal.Completed = request.Completed;

        meal.AddDomainEvent(new MealUpdatedEvent(oldMeal, meal));

        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Results.Ok()
            : Results.UnknownError();
    }
}
