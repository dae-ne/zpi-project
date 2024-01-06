using Microsoft.AspNetCore.Mvc;

namespace Dietly.WebApi.Resources.Meals.GetMeals;

public sealed class GetMealsQueryParams
{
    [FromQuery(Name = "from")]
    public string? From { get; set; }

    [FromQuery(Name = "to")]
    public string? To { get; set; }
}
