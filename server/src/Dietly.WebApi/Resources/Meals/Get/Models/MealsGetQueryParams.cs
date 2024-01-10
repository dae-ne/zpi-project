using Microsoft.AspNetCore.Mvc;

namespace Dietly.WebApi.Resources.Meals.Get.Models;

public sealed class MealsGetQueryParams
{
    [FromQuery(Name = "from")]
    public string? From { get; set; }

    [FromQuery(Name = "to")]
    public string? To { get; set; }
}
