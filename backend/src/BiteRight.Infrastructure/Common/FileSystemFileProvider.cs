using BiteRight.Domain.Abstracts.Common;
using BiteRight.Options;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

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