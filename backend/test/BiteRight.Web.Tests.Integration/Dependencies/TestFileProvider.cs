// # ==============================================================================
// # Solution: BiteRight
// # File: TestFileProvider.cs
// # Author: Łukasz Sobczak
// # Created: 29-02-2024
// # ==============================================================================

using System.IO;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Common;

namespace BiteRight.Web.Tests.Integration.Dependencies;

public class TestFileProvider : IFileProvider
{
    public Task<Stream> GetStream(
        string directory,
        string name
    )
    {
        return Task.FromResult<Stream>(new MemoryStream());
    }
}