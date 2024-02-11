using BiteRight.Application.Dtos.Currencies;

namespace BiteRight.Application.Queries.Currencies.List;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListResponse(IEnumerable<CurrencyDto> Currencies);