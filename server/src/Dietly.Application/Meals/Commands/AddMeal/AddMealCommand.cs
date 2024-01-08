using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Events.Meal;

namespace Dietly.Application.Meals.Commands.AddMeal;

public sealed class AddMealCommand : IRequest<Result<int>>
{
    public int UserId { get; init; }

    public int RecipeId { get; init; }

    public DateTime Date { get; init; }
}

[UsedImplicitly]
internal sealed class AddMealCommandHandler(IAppDbContext db) : IRequestHandler<AddMealCommand, Result<int>>
{
    public async Task<Result<int>> Handle(AddMealCommand request, CancellationToken cancellationToken)
    {
        var recipe = await db.Recipes.FindAsync([request.RecipeId], cancellationToken);

        if (recipe is null)
        {
            return Results.NotFound<int>("Recipe not found");
        }

        if (recipe.UserId != request.UserId)
        {
            return Results.Forbidden<int>("Recipe does not belong to user");
        }

        var meal = new Meal
        {
            RecipeId = request.RecipeId,
            Date = request.Date,
            Completed = false
        };

        meal.AddDomainEvent(new MealAddedEvent(meal));

        db.Meals.Add(meal);
        var changes = await db.SaveChangesAsync(cancellationToken);

        return changes > 0
            ? Results.Created(meal.Id)
            : Results.UnknownError<int>();
    }
}
