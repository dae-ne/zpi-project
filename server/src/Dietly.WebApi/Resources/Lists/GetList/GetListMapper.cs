using System.Collections.Generic;
using Dietly.Application.Lists.Queries.GetList;
using Dietly.Domain.Entities;

namespace Dietly.WebApi.Resources.Lists.GetList;

internal static class GetListMapper
{
    public static GetListQuery ToQuery(this GetListQueryString queryString, int userId)
    {
        var from = string.IsNullOrWhiteSpace(queryString.From)
            ? DateTime.MinValue
            : DateTime.Parse(queryString.From);
        var to = string.IsNullOrWhiteSpace(queryString.To)
            ? DateTime.MaxValue
            : DateTime.Parse(queryString.To);
        return new GetListQuery(userId, from, to);
    }

    public static GetListResponse ToDto(this IList<Ingredient> ingredients)
    {
        return new GetListResponse
        {
            Count = ingredients.Count,
            Ingredients = ingredients.Select(i => i.Name)
        };
    }
}
