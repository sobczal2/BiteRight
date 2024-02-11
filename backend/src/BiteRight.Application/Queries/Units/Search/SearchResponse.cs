using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Units;

namespace BiteRight.Application.Queries.Units.Search;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record SearchResponse(PaginatedList<UnitDto> Units);