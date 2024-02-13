// # ==============================================================================
// # Solution: BiteRight
// # File: SearchResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Units;

#endregion

namespace BiteRight.Application.Queries.Units.Search;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record SearchResponse(PaginatedList<UnitDto> Units);