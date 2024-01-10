using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.Recipe;
using Microsoft.Extensions.Logging;

namespace Dietly.Application.Recipes.EventHandlers;

[UsedImplicitly]
internal sealed class RemoveOldFoodImageOnRecipeUpdatedEventHandler(
    ILogger<RemoveOldFoodImageOnRecipeUpdatedEventHandler> logger,
    IImageStorage imageStorage)
    : INotificationHandler<RecipeUpdatedEvent>
{
    public async Task Handle(RecipeUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing old food image for updated recipe with id {RecipeId}", notification.NewItem.Id);

        var recipeId = notification.NewItem.Id;
        var fileName = notification.OldItem.ImageUrl?.Split('/').Last();
        var newFileName = notification.NewItem.ImageUrl?.Split('/').Last();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            logger.LogInformation("Recipe with id {RecipeId} has no previous food image", recipeId);
            return;
        }

        if (fileName == newFileName)
        {
            logger.LogInformation("Recipe with id {RecipeId} has not changed food image", recipeId);
            return;
        }

        try
        {
            await imageStorage.DeleteAsync(fileName, cancellationToken);
            logger.LogInformation("Old food image for recipe with id {RecipeId} removed", recipeId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while removing old food image for recipe with id {RecipeId}. File name: {FileName}", recipeId, fileName);
        }
    }
}
