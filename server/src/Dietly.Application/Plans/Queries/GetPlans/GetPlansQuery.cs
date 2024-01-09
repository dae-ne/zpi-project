using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

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
            return Results.NotFound<IList<DayPlan>>("Plans not found");
        }

        var plans = meals
            .ToDayPlans(request.StartDate, request.EndDate, request.UserId)
            .ToList();

        return Results.Ok<IList<DayPlan>>(plans);
    }
}
