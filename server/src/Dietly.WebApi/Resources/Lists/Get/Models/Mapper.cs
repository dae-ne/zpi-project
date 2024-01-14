using System.Collections.Generic;
using Dietly.Application.Lists.Queries.GetList;
using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Lists.Get.Models;

internal static class Mapper
{
    public static GetListQuery ToQuery(this ListGetQueryString queryString, int userId)
    {
        var from = string.IsNullOrWhiteSpace(queryString.From)
            ? DateTime.MinValue
            : DateTime.Parse(queryString.From);
        var to = string.IsNullOrWhiteSpace(queryString.To)
            ? DateTime.MaxValue
            : DateTime.Parse(queryString.To);
        return new GetListQuery(userId, from, to);
    }

    public static ListGetResponse ToDto(this IList<Ingredient> ingredients)
    {
        return new ListGetResponse
        {
            Count = ingredients.Count,
            Ingredients = ingredients.Select(i => i.Name)
        };
    }
}
