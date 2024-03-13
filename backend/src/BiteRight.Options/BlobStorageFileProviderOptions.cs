// # ==============================================================================
// # Solution: BiteRight
// # File: BlobStorageFileProviderOptions.cs
// # Author: Łukasz Sobczak
// # Created: 04-03-2024
// # ==============================================================================

namespace BiteRight.Options;

public class BlobStorageFileProviderOptions
{
    public const string SectionName = "BlobStorageFileProvider";
    public string ContainerName { get; set; } = default!;
}