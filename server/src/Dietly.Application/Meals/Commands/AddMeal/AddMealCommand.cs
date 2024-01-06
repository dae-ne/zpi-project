using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.Meal;

namespace Dietly.Application.Meals.Commands.AddMeal;

public sealed class AddMealCommand : IRequest<int>
{
    public int UserId { get; init; }

    public int RecipeId { get; init; }

    public DateTime Date { get; init; }
}

[UsedImplicitly]
internal sealed class AddMealCommandHandler(IAppDbContext db) : IRequestHandler<AddMealCommand, int>
{
    public async Task<int> Handle(AddMealCommand request, CancellationToken cancellationToken)
    {
        var recipe = await db.Recipes
            .FindAsync(new object[] { request.RecipeId }, cancellationToken);

        if (recipe is null)
        {
            // TODO: handle not found
        }

        // TODO: remove pragma
#pragma warning disable CS8602 // Dereference of a possibly null reference.
        if (recipe.UserId != request.UserId)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        {
            // TODO: handle unauthorized
        }

        var meal = new Meal
        {
            RecipeId = request.RecipeId,
            Date = request.Date,
            Completed = false
        };

        meal.AddDomainEvent(new MealAddedEvent(meal));

        db.Meals.Add(meal);
        await db.SaveChangesAsync(cancellationToken);

        return meal.Id;
    }
}