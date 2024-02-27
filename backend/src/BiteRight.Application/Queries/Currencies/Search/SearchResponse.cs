// # ==============================================================================
// # Solution: BiteRight
// # File: SearchResponse.cs
// # Author: Łukasz Sobczak
// # Created: 20-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Currencies;

#endregion

namespace BiteRight.Application.Queries.Currencies.Search;

public record SearchResponse(PaginatedList<CurrencyDto> Currencies);