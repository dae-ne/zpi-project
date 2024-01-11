using Dietly.Application.Common.Results;
using Dietly.Domain.Extensions;

namespace Dietly.Application.Plans.Queries.GetPlan;

public sealed record GetPlanQuery(DateOnly Day, int UserId) : IRequest<Result<DayPlan>>;

[UsedImplicitly]
internal sealed class GetPlanQueryHandler(IAppDbContext db) : IRequestHandler<GetPlanQuery, Result<DayPlan>>
{
    public async Task<Result<DayPlan>> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .AsNoTracking()
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) == request.Day)
            .ToListAsync(cancellationToken);

        if (meals.Count == 0)
        {
            return Errors.NotFound("Plans not found");
        }

        var plan = meals.ToDayPlan(request.Day, request.UserId);
        return plan;
    }
}
