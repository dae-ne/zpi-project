using Dietly.Application.Common.Results;
using Dietly.Domain.Extensions;

namespace Dietly.Application.Plans.Queries.GetPlans;

public sealed class GetPlansQuery : IRequest<Result<IList<DayPlan>>>
{
    public int UserId { get; init; }

    public DateOnly StartDate { get; init; } = DateOnly.MinValue;

    public DateOnly EndDate { get; init; } = DateOnly.MaxValue;
}

[UsedImplicitly]
internal sealed class GetPlansQueryHandler(IAppDbContext db) : IRequestHandler<GetPlansQuery, Result<IList<DayPlan>>>
{
    public async Task<Result<IList<DayPlan>>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .AsNoTracking()
            .Include(m => m.Recipe)
            .Where(m => m.Recipe.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) >= request.StartDate &&
                        DateOnly.FromDateTime(m.Date) <= request.EndDate)
            .ToListAsync(cancellationToken);

        if (meals.Count == 0)
        {
            return Errors.NotFound("Plans not found");
        }

        var plans = meals
            .ToDayPlans(request.StartDate, request.EndDate, request.UserId)
            .ToList();

        return plans;
    }
}
