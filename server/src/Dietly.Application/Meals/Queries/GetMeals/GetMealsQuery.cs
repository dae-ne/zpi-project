using Dietly.Application.Common.Results;

namespace Dietly.Application.Meals.Queries.GetMeals;

public sealed class GetMealsQuery : IRequest<Result<IList<Meal>>>
{
    public int UserId { get; init; }

    public DateOnly StartDate { get; init; } = DateOnly.MinValue;

    public DateOnly EndDate { get; init; } = DateOnly.MaxValue;
}

[UsedImplicitly]
internal sealed class GetMealsQueryHandler(IAppDbContext db) : IRequestHandler<GetMealsQuery, Result<IList<Meal>>>
{
    public async Task<Result<IList<Meal>>> Handle(GetMealsQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .AsNoTracking()
            .Include(m => m.Recipe).ThenInclude(r => r.Ingredients)
            .Include(m => m.Recipe).ThenInclude(r => r.Directions)
            .Include(m => m.Recipe).ThenInclude(r => r.Tags)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) >= request.StartDate &&
                        DateOnly.FromDateTime(m.Date) <= request.EndDate)
            .ToListAsync(cancellationToken);

        return meals.Count > 0
            ? meals
            : Errors.NotFound("Meals not found");
    }
}
