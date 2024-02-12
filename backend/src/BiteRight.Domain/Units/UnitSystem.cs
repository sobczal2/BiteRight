// # ==============================================================================
// # Solution: BiteRight
// # File: UnitSystem.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Domain.Units;

[Flags]
public enum UnitSystem
{
    Metric = 1,
    Imperial = 2,
    Both = Metric | Imperial
}