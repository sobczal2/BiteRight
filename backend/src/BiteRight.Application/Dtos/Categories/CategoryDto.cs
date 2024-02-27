// # ==============================================================================
// # Solution: BiteRight
// # File: CategoryDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Categories;
using BiteRight.Domain.Languages;

#endregion

namespace BiteRight.Application.Dtos.Categories;

public class CategoryDto
{
    public CategoryDto(
        Guid id,
        string name
    )
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }

    public static CategoryDto FromDomain(
        Category category,
        LanguageId languageId
    )
    {
        return new CategoryDto(
            category.Id,
            category.GetName(languageId)
        );
    }
}