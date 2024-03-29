namespace Dietly.Application.Common.Interfaces;

public interface IStorageClient
{
    Task UploadAsync(string fileName, byte[] data, CancellationToken cancellationToken);

    Task<byte[]> GetAsync(string fileName, CancellationToken cancellationToken);

    Task DeleteAsync(string fileName, CancellationToken cancellationToken);
}

public interface IAvatarStorage : IStorageClient;

public interface IImageStorage : IStorageClient;
