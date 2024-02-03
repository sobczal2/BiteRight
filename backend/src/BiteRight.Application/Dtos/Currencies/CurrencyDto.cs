namespace BiteRight.Application.Dtos.Currencies;

public class CurrencyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Symbol { get; set; } = default!;
    // ReSharper disable once InconsistentNaming
    public string ISO4217Code { get; set; } = default!;
}