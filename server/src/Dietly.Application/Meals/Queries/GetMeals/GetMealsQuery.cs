using Dietly.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Meals.Queries.GetMeals;

public sealed class GetMealsQuery : IRequest<IList<Meal>>
{
    public int UserId { get; init; }

    public DateOnly StartDate { get; init; } = DateOnly.MinValue;

    public DateOnly EndDate { get; init; } = DateOnly.MaxValue;
}

[UsedImplicitly]
internal sealed class GetMealsQueryHandler(IAppDbContext db) : IRequestHandler<GetMealsQuery, IList<Meal>>
{
    public async Task<IList<Meal>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) >= request.StartDate &&
                        DateOnly.FromDateTime(m.Date) <= request.EndDate)
            .ToListAsync(cancellationToken);

        return meals;
    }
}
