using System.Collections.Generic;
using Dietly.Application.Plans.Queries.GetPlans;
using Dietly.Domain.Entities;
using Dietly.WebApi.Resources.Meals.Get.Models;

namespace Dietly.WebApi.Resources.Plans.Get.Models;

internal static class Mapper
{
    public static PlanGetResponse ToDto(this DayPlan plan) => new()
    {
        Date = plan.Date.ToString("yyyy-MM-dd"),
        TotalCalories = plan.TotalCalories,
        ConsumedCalories = plan.ConsumedCalories,
        Meals = plan.Meals.Select(x => x.ToDto())
    };

    public static GetPlansQuery ToQuery(this PlansGetQueryParams queryParams, int userId) => new()
    {
        UserId = userId,
        StartDate = DateOnly.Parse(queryParams.From ?? DateOnly.MinValue.ToString()),
        EndDate = DateOnly.Parse(queryParams.To ?? DateOnly.MaxValue.ToString())
    };

    public static PlansGetResponse ToDto(this IList<DayPlan> plans)
    {
        var response = new PlansGetResponse
        {
            Count = plans.Count,
            Data = plans.Select(p => p.ToDto())
        };

        return response;
    }
}
