using BiteRight.Application.Dtos.Currencies;

namespace BiteRight.Application.Queries.Currencies.List;

public class ListResponse
{
    public ListResponse(
        IEnumerable<CurrencyDto> currencies
    )
    {
        Currencies = currencies;
    }

    public IEnumerable<CurrencyDto> Currencies { get; }
}