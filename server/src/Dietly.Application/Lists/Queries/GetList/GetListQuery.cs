using Dietly.Application.Common.Interfaces;
using Dietly.Application.Common.Result;
using Microsoft.EntityFrameworkCore;

namespace Dietly.Application.Lists.Queries.GetList;

public sealed record GetListQuery(int UserId, DateTime From, DateTime To) : IRequest<Result<List<Ingredient>>>;

[UsedImplicitly]
internal sealed class GetListQueryHandler(IAppDbContext db) : IRequestHandler<GetListQuery, Result<List<Ingredient>>>
{
    public async Task<Result<List<Ingredient>>> Handle(GetListQuery request, CancellationToken cancellationToken)
    {
        var meals = await db.Meals
            .AsNoTracking()
            .Include(m => m.Recipe)
            .ThenInclude(r => r.Ingredients)
            .Where(m => m.Recipe!.UserId == request.UserId)
            .Where(m => m.Date >= request.From && m.Date <= request.To)
            .ToListAsync(cancellationToken);

        var ingredients = meals
            .SelectMany(m => m.Recipe!.Ingredients)
            .GroupBy(i => i.Id)
            .Select(g => g.First())
            .ToList();

        return Results.Ok(ingredients);
    }
}
