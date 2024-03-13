// # ==============================================================================
// # Solution: BiteRight
// # File: IFileProvider.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.IO;
using System.Threading.Tasks;

#endregion

namespace BiteRight.Domain.Abstracts.Common;

public interface IFileProvider
{
    Task<Stream> GetStream(
        string directory,
        string name
    );
}