using Microsoft.AspNetCore.Mvc;

namespace Recipes.WebApi.Endpoints.Plans.GetPlans;

public sealed class GetPlansQueryParams
{
    [FromQuery(Name = "from")]
    public string? From { get; init; }

    [FromQuery(Name = "to")]
    public string? To { get; init; }
}
