// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrentResponse.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using BiteRight.Application.Dtos.Products;

#endregion

namespace BiteRight.Application.Queries.Products.ListCurrent;

public record ListCurrentResponse(IEnumerable<SimpleProductDto> Products);