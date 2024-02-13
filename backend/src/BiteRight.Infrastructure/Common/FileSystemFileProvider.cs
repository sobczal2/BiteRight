// # ==============================================================================
// # Solution: BiteRight
// # File: FileSystemFileProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.IO;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Options;
using Microsoft.Extensions.Options;

#endregion

namespace BiteRight.Infrastructure.Common;

public class FileSystemFileProvider : IFileProvider
{
    private readonly FileSystemFileProviderOptions _options;

    public FileSystemFileProvider(
        IOptions<FileSystemFileProviderOptions> options
    )
    {
        _options = options.Value;
    }

    public Stream GetStream(
        string directory,
        string name
    )
    {
        var path = Path.Combine(_options.RootPath, directory, name);
        return File.OpenRead(path);
    }
}