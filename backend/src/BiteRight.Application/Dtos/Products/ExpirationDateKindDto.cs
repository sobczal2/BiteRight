// # ==============================================================================
// # Solution: BiteRight
// # File: ExpirationDateKindDto.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

namespace BiteRight.Application.Dtos.Products;

public enum ExpirationDateKindDto
{
    Unknown = 0,
    Infinite = 1,
    BestBefore = 2,
    UseBy = 3
}