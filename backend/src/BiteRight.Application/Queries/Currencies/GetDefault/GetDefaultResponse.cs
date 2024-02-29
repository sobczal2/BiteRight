// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultResponse.cs
// # Author: Łukasz Sobczak
// # Created: 29-02-2024
// # ==============================================================================

using BiteRight.Application.Dtos.Currencies;

namespace BiteRight.Application.Queries.Currencies.GetDefault;

public class GetDefaultResponse
{
    public CurrencyDto Currency { get; set; }
    
    public GetDefaultResponse(CurrencyDto currency)
    {
        Currency = currency;
    }
}