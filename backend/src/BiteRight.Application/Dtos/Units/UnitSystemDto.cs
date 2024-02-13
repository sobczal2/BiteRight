// # ==============================================================================
// # Solution: BiteRight
// # File: UnitSystemDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

namespace BiteRight.Application.Dtos.Units;

public enum UnitSystemDto
{
    Metric = 1,
    Imperial = 2,
    Both = Metric | Imperial
}