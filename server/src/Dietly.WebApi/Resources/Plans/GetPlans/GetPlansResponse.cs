using System.Collections.Generic;
using Dietly.WebApi.Resources.Plans.GetPlan;

namespace Dietly.WebApi.Resources.Plans.GetPlans;

public sealed class GetPlansResponse
{
    public int Count { get; init; }

    public IEnumerable<GetPlanResponse> Data { get; init; } = Enumerable.Empty<GetPlanResponse>();
}
