using Dietly.Domain.Events.User;
using Microsoft.Extensions.Logging;

namespace Dietly.Application.Users.EventHandlers;

[UsedImplicitly]
internal sealed class RemoveAvatarOnUserRemovedEventHandler(
    ILogger<RemoveAvatarOnUserRemovedEventHandler> logger,
    IAvatarStorage avatarStorage)
    : INotificationHandler<UserRemovedEvent>
{
    public async Task Handle(UserRemovedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing avatar for removed user with id {UserId}", notification.Item.Id);

        var userId = notification.Item.Id;
        var fileName = notification.Item.AvatarUrl?.Split('/').Last();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            logger.LogInformation("User with id {UserId} has no avatar", userId);
            return;
        }

        try
        {
            await avatarStorage.DeleteAsync(fileName, cancellationToken);
            logger.LogInformation("Avatar for user with id {UserId} removed", userId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while removing avatar for user with id {UserId}. File name: {FileName}", userId, fileName);
        }
    }
}
