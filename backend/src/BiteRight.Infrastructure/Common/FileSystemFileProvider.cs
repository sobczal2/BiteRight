// # ==============================================================================
// # Solution: BiteRight
// # File: FileSystemFileProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.IO;
using System.Threading.Tasks;
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

    public Task<Stream> GetStream(
        string directory,
        string name
    )
    {
        var path = Path.Combine(_options.RootPath, directory, name);
        return Task.FromResult<Stream>(File.OpenRead(path));
    }
}