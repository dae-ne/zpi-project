using Dietly.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Queries.GetMeal;

public sealed record GetMealQuery(int MealId, int UserId) : IRequest<Meal>;

[UsedImplicitly]
internal sealed class GetMealQueryHandler(IAppDbContext db) : IRequestHandler<GetMealQuery, Meal>
{
    public async Task<Meal> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
        var meal = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .SingleOrDefaultAsync(m => m.Id == request.MealId, cancellationToken);

        if (meal is null)
        {
            // TODO: handle not found
        }

        return meal;
    }
}
