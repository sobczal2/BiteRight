namespace BiteRight.Application.Dtos.Currencies;

public class CurrencyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    public string Code { get; set; } = default!;
}