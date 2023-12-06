using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Recipes.Application.Common.Interfaces;

namespace Recipes.Infrastructure.Blob;

internal sealed class AvatarBlobStorage(BlobServiceClient client, IOptions<ImageStorageOptions> options)
    : BlobStorageBaseClient(client, options.Value.AvatarContainerName), IAvatarStorage;
