// # ==============================================================================
// # Solution: BiteRight
// # File: BlobStorageFileProvider.cs
// # Author: Łukasz Sobczak
// # Created: 04-03-2024
// # ==============================================================================

using System;
using System.IO;
using System.Threading.Tasks;
using Azure.Identity;
using Azure.Storage.Blobs;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BiteRight.Infrastructure.Common;

public class BlobStorageFileProvider : IFileProvider
{
    private readonly BlobContainerClient _blobClient;

    public BlobStorageFileProvider(
        IOptions<BlobStorageFileProviderOptions> options,
        IConfiguration configuration
    )
    {
        _blobClient = new BlobContainerClient(
            configuration["ConnectionStrings:BlobStorage"],
            blobContainerName: options.Value.ContainerName
        );
    }

    public Task<Stream> GetStream(
        string directory,
        string name
    )
    {
        var blobClient = _blobClient.GetBlobClient($"{directory}/{name}");
        return blobClient.OpenReadAsync();
    }
}