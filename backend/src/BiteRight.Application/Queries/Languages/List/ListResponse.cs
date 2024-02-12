// # ==============================================================================
// # Solution: BiteRight
// # File: ListResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using BiteRight.Application.Dtos.Languages;

#endregion

namespace BiteRight.Application.Queries.Languages.List;

// ReSharper disable once NotAccessedPositionalProperty.Global
public record ListResponse(IEnumerable<LanguageDto> Languages);