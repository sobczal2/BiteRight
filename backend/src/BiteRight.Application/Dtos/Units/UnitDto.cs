// # ==============================================================================
// # Solution: BiteRight
// # File: UnitDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Dtos.Units;

public class UnitDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Abbreviation { get; set; } = default!;
    public UnitSystemDto UnitSystem { get; set; }
}