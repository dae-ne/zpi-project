using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Queries.GetMeal;

public sealed record GetMealQuery(int MealId, int UserId) : IRequest<Result<Meal>>;

[UsedImplicitly]
internal sealed class GetMealQueryHandler(IAppDbContext db) : IRequestHandler<GetMealQuery, Result<Meal>>
{
    public async Task<Result<Meal>> Handle(GetMealQuery request, CancellationToken cancellationToken)
    {
        var meal = await db.Meals
            .AsNoTracking()
            .Include(m => m.Recipe).ThenInclude(r => r.Ingredients)
            .Include(m => m.Recipe).ThenInclude(r => r.Directions)
            .Include(m => m.Recipe).ThenInclude(r => r.Tags)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .SingleOrDefaultAsync(m => m.Id == request.MealId, cancellationToken);

        return meal is not null
            ? Results.Ok(meal)
            : Results.NotFound<Meal>("Meal not found");
    }
}
