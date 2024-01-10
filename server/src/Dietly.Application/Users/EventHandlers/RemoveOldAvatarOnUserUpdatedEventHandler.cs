using Dietly.Application.Common.Interfaces;
using Dietly.Domain.Events.User;
using Microsoft.Extensions.Logging;

namespace Dietly.Application.Users.EventHandlers;

[UsedImplicitly]
internal sealed class RemoveOldAvatarOnUserUpdatedEventHandler(
    ILogger<RemoveOldAvatarOnUserUpdatedEventHandler> logger,
    IAvatarStorage avatarStorage)
    : INotificationHandler<UserUpdatedEvent>
{
    public async Task Handle(UserUpdatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Removing old avatar for user with id {UserId}", notification.NewItem.Id);

        var userId = notification.NewItem.Id;
        var fileName = notification.OldItem.AvatarUrl?.Split('/').Last();
        var newFileName = notification.NewItem.AvatarUrl?.Split('/').Last();

        if (string.IsNullOrWhiteSpace(fileName))
        {
            logger.LogInformation("User with id {UserId} has no previous avatar", userId);
            return;
        }

        if (fileName == newFileName)
        {
            logger.LogInformation("User with id {UserId} has not changed avatar", userId);
            return;
        }

        try
        {
            await avatarStorage.DeleteAsync(fileName, cancellationToken);
            logger.LogInformation("Old avatar for user with id {UserId} removed", userId);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while removing old avatar for user with id {UserId}. File name: {FileName}", userId, fileName);
        }
    }
}
