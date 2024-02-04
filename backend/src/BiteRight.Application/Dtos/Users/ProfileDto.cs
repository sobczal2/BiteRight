namespace BiteRight.Application.Dtos.Users;

public class ProfileDto
{
    public Guid CurrencyId { get; set; }
    public string CurrencyName { get; set; } = default!;
}