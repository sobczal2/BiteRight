#region

using System.IO;

#endregion

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public record GetPhotoResponse(Stream PhotoStream, string ContentType, string FileName);