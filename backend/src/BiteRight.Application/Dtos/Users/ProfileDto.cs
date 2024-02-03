namespace BiteRight.Application.Dtos.Users;

public class ProfileDto
{
    public Guid CountryId { get; set; }
    public string CountryName { get; set; } = default!;
    public Guid LanguageId { get; set; }
    public string LanguageName { get; set; } = default!;
    public string LanguageCode { get; set; } = default!;
    public Guid CurrencyId { get; set; }
    public string CurrencyName { get; set; } = default!;
}