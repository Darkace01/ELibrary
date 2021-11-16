using Azure.Storage.Blobs;
using ELibrary.Service.Contract;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ELibrary.Service.Implementation;

public class FileStorageService : IFileStorageService
{
    private readonly string connectionString;
    public FileStorageService(IConfiguration configuration)
    {
        connectionString = configuration.GetConnectionString("AzureStorageConnection");
    }

    public async Task DeleteFile(string fileRoute, string containerName)
    {
        if (string.IsNullOrEmpty(fileRoute))
            return;

        var client = new BlobContainerClient(connectionString, containerName);
        await client.CreateIfNotExistsAsync();
        var fileName = Path.GetFileName(fileRoute);
        var blob = client.GetBlobClient(fileName);
        await blob.DeleteIfExistsAsync();
    }

    public async Task<string> EditFile(string containerName, IFormFile file, string fileRoute)
    {
        await DeleteFile(fileRoute, containerName);
        return await SaveFile(containerName, file);
    }

    public async Task<string> SaveFile(string containerName, IFormFile file)
    {
        var client = new BlobContainerClient(connectionString, containerName);
        await client.CreateIfNotExistsAsync();
        client.SetAccessPolicy(Azure.Storage.Blobs.Models.PublicAccessType.Blob);

        var extension = Path.GetExtension(file.FileName);
        var fileName = $"{Guid.NewGuid()}{extension}";
        var blob = client.GetBlobClient(fileName);
        await blob.UploadAsync(file.OpenReadStream());
        return blob.Uri.ToString();
    }
}
