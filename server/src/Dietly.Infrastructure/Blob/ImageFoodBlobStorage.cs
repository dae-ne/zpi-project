using Azure.Storage.Blobs;
using Microsoft.Extensions.Options;
using Dietly.Application.Common.Interfaces;

namespace Dietly.Infrastructure.Blob;

internal sealed class ImageFoodBlobStorage(BlobServiceClient client, IOptions<ImageStorageOptions> options)
    : BlobStorageBaseClient(client, options.Value.ImageContainerName), IImageStorage;
