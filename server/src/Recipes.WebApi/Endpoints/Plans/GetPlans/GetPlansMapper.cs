using System.Collections.Generic;
using Recipes.Domain.Entities;
using Recipes.WebApi.Endpoints.Plans.GetPlan;

namespace Recipes.WebApi.Endpoints.Plans.GetPlans;

internal static class GetPlansMapper
{
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
