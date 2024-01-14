using Dietly.Application.Lists.Commands.SendEmailWithList;

namespace Dietly.WebApi.Resources.Lists.Post.Models;

internal static class Mapper
{
    public static SendEmailWithListCommand ToCommand(this SendEmailWithListRequest request, int userId) => new()
    {
        UserId = userId,
        IngredientIds = request.IngredientIds
    };
}
