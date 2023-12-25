namespace BiteRight.Application.Dtos.Languages;

public class LanguageDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Code { get; set; } = default!;
}