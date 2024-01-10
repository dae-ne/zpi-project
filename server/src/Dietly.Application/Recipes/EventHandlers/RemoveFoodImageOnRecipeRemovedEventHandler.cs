using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.Recipe;
using Microsoft.Extensions.Logging;

namespace Dietly.Application.Recipes.EventHandlers;

[UsedImplicitly]
internal sealed class RemoveFoodImageOnRecipeRemovedEventHandler(
    ILogger<RemoveFoodImageOnRecipeRemovedEventHandler> logger,
    IImageStorage imageStorage)
    : INotificationHandler<RecipeRemovedEvent>
{
    public async Task Handle(RecipeRemovedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing food image for removed recipe with id {RecipeId}", notification.Item.Id);

        var recipeId = notification.Item.Id;
        var fileName = notification.Item.ImageUrl?.Split('/').Last();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            logger.LogInformation("Recipe with id {RecipeId} has no food image", recipeId);
            return;
        }

        try
        {
            await imageStorage.DeleteAsync(fileName, cancellationToken);
            logger.LogInformation("Food image for recipe with id {RecipeId} removed", recipeId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while removing food image for recipe with id {RecipeId}. File name: {FileName}", recipeId, fileName);
        }
    }
}
