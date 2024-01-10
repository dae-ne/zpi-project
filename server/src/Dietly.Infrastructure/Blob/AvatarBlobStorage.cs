using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;

namespace Dietly.Infrastructure.Blob;

internal sealed class AvatarBlobStorage(BlobServiceClient client, IOptions<ImageStorageOptions> options)
    : BlobStorageBaseClient(client, options.Value.AvatarContainerName), IAvatarStorage;
