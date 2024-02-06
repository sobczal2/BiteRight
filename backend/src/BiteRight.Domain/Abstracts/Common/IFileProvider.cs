namespace BiteRight.Domain.Abstracts.Common;

public interface IFileProvider
{
    Stream GetStream(string directory, string name);
}