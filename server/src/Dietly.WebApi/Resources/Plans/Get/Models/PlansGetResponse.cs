using System.Collections.Generic;

namespace Dietly.WebApi.Resources.Plans.Get.Models;

public sealed class PlansGetResponse
{
    public int Count { get; init; }

    public IEnumerable<PlanGetResponse> Data { get; init; } = Enumerable.Empty<PlanGetResponse>();
}
