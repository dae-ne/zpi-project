using System.Collections.Generic;
using Dietly.WebApi.Endpoints.Plans.GetPlan;

namespace Dietly.WebApi.Endpoints.Plans.GetPlans;

public sealed class GetPlansResponse
{
    public int Count { get; init; }

    public IEnumerable<GetPlanResponse> Data { get; init; } = Enumerable.Empty<GetPlanResponse>();
}
