using Azure.Storage.Blobs;
using Dietly.Application.Common.Interfaces;
using Microsoft.Extensions.Options;

namespace Dietly.Infrastructure.Blob;

internal sealed class ImageFoodBlobStorage(BlobServiceClient client, IOptions<ImageStorageOptions> options)
    : BlobStorageBaseClient(client, options.Value.ImageContainerName), IImageStorage;
