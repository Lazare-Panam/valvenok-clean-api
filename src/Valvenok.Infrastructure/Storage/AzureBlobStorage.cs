using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Options;
using Valvenok.Core.Interfaces;
using Valvenok.Infrastructure.Data.Config.Settings;

namespace Valvenok.Infrastructure.Storage
{
    public class AzureBlobStorage : IFileStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly AzureStorageSettings _settings;
        public AzureBlobStorage(BlobServiceClient blobServiceClient, IOptions<AzureStorageSettings> settings)
        {
            _blobServiceClient = blobServiceClient;
            _settings = settings.Value;
        }
        public async Task<string> UploadFile(Stream fileStream, string fileName, string contentType, CancellationToken ct)
        {
            var uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(_settings.ContainerName);
            BlobClient blobClient = containerClient.GetBlobClient(uniqueFileName);
            await blobClient.UploadAsync(fileStream, new BlobHttpHeaders { ContentType = contentType });

            return blobClient.Uri.ToString();
        }
    }
}
