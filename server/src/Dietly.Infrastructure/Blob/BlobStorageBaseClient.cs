using Azure.Storage.Blobs;

namespace Dietly.Infrastructure.Blob;

internal abstract class BlobStorageBaseClient(BlobServiceClient client, string containerName) : IStorageClient
{
    public async Task UploadAsync(string fileName, byte[] data, CancellationToken cancellationToken)
    {
        var blob = await GetBlobClientAsync(fileName, cancellationToken);
        var binaryData = new BinaryData(data);
        await blob.UploadAsync(binaryData, cancellationToken);
    }

    public async Task<byte[]> GetAsync(string fileName, CancellationToken cancellationToken)
    {
        var blob = await GetBlobClientAsync(fileName, cancellationToken);
        var response = await blob.DownloadContentAsync(cancellationToken);
        return response.Value.Content.ToArray();
    }

    public async Task DeleteAsync(string fileName, CancellationToken cancellationToken)
    {
        var blob = await GetBlobClientAsync(fileName, cancellationToken);
        await blob.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }

    private async Task<BlobClient> GetBlobClientAsync(string fileName, CancellationToken cancellationToken)
    {
        var container = client.GetBlobContainerClient(containerName);
        await container.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        return container.GetBlobClient(fileName);
    }
}
