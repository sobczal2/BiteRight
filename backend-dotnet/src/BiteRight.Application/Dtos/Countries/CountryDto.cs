namespace BiteRight.Application.Dtos.Countries;

public class CountryDto
{
    public Guid Id { get; set; }
    public string NativeName { get; set; } = default!;
    public string EnglishName { get; set; } = default!;
    public string Alpha2Code { get; set; } = default!;
    public Guid OfficialLanguageId { get; set; }
}