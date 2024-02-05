using BiteRight.Application.Dtos.Countries;

namespace BiteRight.Application.Queries.Countries.List;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListResponse(IEnumerable<CountryDto> Countries);