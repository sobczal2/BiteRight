// # ==============================================================================
// # Solution: BiteRight
// # File: FileSystemFileProviderOptions.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

namespace BiteRight.Options;

public class FileSystemFileProviderOptions
{
    public const string SectionName = "FileSystemFileProvider";
    public string RootPath { get; set; } = default!;
}