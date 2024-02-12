// # ==============================================================================
// # Solution: BiteRight
// # File: GetPhotoResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.IO;

#endregion

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoResponse
{
    public Stream PhotoStream { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string FileName { get; set; } = default!;
}