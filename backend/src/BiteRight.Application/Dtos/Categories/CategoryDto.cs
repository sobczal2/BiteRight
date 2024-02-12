// # ==============================================================================
// # Solution: BiteRight
// # File: CategoryDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;

#endregion

namespace BiteRight.Application.Dtos.Categories;

public class CategoryDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
}