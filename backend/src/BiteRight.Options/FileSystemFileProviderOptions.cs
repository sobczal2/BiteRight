namespace BiteRight.Options;

public class FileSystemFileProviderOptions
{
    public const string SectionName = "FileSystemFileProvider";
    public string RootPath { get; set; } = default!;
}