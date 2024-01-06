using System.Collections.Generic;
using Dietly.Application.Plans.Queries.GetPlans;
using Dietly.Domain.Entities;
using Dietly.WebApi.Endpoints.Plans.GetPlan;

namespace Dietly.WebApi.Endpoints.Plans.GetPlans;

internal static class GetPlansMapper
{
    public static GetPlansQuery ToQuery(this GetPlansQueryParams queryParams, int userId) => new()
    {
        UserId = userId,
        StartDate = DateOnly.Parse(queryParams.From ?? DateOnly.MinValue.ToString()),
        EndDate = DateOnly.Parse(queryParams.To ?? DateOnly.MaxValue.ToString())
    };

    public static GetPlansResponse ToDto(this IList<DayPlan> plans)
    {
        var response = new GetPlansResponse
        {
            Count = plans.Count,
            Data = plans.Select(p => p.ToDto())
        };

        return response;
    }
}
