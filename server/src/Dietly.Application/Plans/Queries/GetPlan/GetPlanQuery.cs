using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Dietly.Domain.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Plans.Queries.GetPlan;

public sealed record GetPlanQuery(DateOnly Day, int UserId) : IRequest<Result<DayPlan>>;

[UsedImplicitly]
internal sealed class GetPlanQueryHandler(IAppDbContext db) : IRequestHandler<GetPlanQuery, Result<DayPlan>>
{
    public async Task<Result<DayPlan>> Handle(GetPlanQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .Include(m => m.Recipe)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => DateOnly.FromDateTime(m.Date) == request.Day)
            .ToListAsync(cancellationToken);

        if (meals.Count == 0)
        {
            return Results.NotFound<DayPlan>("Plans not found");
        }

        var plan = meals.ToDayPlan(request.Day, request.UserId);

        return Results.Ok(plan);
    }
}
