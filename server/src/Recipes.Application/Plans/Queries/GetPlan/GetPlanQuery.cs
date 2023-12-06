using Microsoft.EntityFrameworkCore;
using Recipes.Application.Common.Interfaces;
using Recipes.Domain.Extensions;

namespace Recipes.Application.Plans.Queries.GetPlan;

public sealed record GetPlanQuery(DateOnly Day, int UserId) : IRequest<DayPlan>;

[UsedImplicitly]
internal sealed class GetPlanQueryHandler(IAppDbContext db) : IRequestHandler<GetPlanQuery, DayPlan>
{
    public async Task<DayPlan> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) == request.Day)
            .ToListAsync(cancellationToken);

        var plan = meals.ToDayPlan(request.Day, request.UserId);

        return plan;
    }
}
