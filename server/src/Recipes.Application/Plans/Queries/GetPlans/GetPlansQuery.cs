using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;
using Recipes.Domain.Extensions;

namespace Recipes.Application.Plans.Queries.GetPlans;

public sealed class GetPlansQuery : IRequest<IList<DayPlan>>
{
    public int UserId { get; init; }
    
    public DateOnly StartDate { get; init; } = DateOnly.MinValue;
    
    public DateOnly EndDate { get; init; } = DateOnly.MaxValue;
}

[UsedImplicitly]
internal sealed class GetPlansQueryHandler(IAppDbContext db) : IRequestHandler<GetPlansQuery, IList<DayPlan>>
{
    public async Task<IList<DayPlan>> Handle(GetPlansQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) >= request.StartDate &&
                        DateOnly.FromDateTime(m.Date) <= request.EndDate)
            .ToListAsync(cancellationToken);

        var plans = meals
            .ToDayPlans(request.StartDate, request.EndDate, request.UserId)
            .ToList();

        return plans;
    }
}
