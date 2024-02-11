using System.Collections.Generic;
using BiteRight.Application.Dtos.Languages;

namespace BiteRight.Application.Queries.Languages.List;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListResponse(IEnumerable<LanguageDto> Languages);