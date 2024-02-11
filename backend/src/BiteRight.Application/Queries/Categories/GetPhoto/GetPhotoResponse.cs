using System.IO;

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoResponse
{
    public Stream PhotoStream { get; set; } = default!;
    public string ContentType { get; set; } = default!;
    public string FileName { get; set; } = default!;
}