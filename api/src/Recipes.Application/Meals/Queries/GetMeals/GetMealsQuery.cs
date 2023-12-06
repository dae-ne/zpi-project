using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;

namespace Recipes.Application.Meals.Queries.GetMeals;

public sealed record GetMealsQuery(int UserId) : IRequest<IList<Meal>>;

[UsedImplicitly]
internal sealed class GetMealsQueryHandler(IAppDbContext db) : IRequestHandler<GetMealsQuery, IList<Meal>>
{
    public async Task<IList<Meal>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .ToListAsync(cancellationToken);
        
        return meals;
    }
}
