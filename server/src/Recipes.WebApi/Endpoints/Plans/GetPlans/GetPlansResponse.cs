using System.Collections.Generic;
using Recipes.WebApi.Endpoints.Plans.GetPlan;

namespace Recipes.WebApi.Endpoints.Plans.GetPlans;

public sealed class GetPlansResponse
{
    public int Count { get; init; }
    
    public IEnumerable<GetPlanResponse> Data { get; init; } = Enumerable.Empty<GetPlanResponse>();
}
