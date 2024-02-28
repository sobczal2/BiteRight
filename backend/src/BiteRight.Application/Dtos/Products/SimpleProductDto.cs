// # ==============================================================================
// # Solution: BiteRight
// # File: SimpleProductDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Products;

#endregion

namespace BiteRight.Application.Dtos.Products;

public class SimpleProductDto
{
    public SimpleProductDto(
        Guid id,
        string name,
        ExpirationDateKindDto expirationDateKind,
        DateOnly? expirationDate,
        Guid categoryId,
        DateTime addedDateTime,
        double currentAmount,
        double maxAmount,
        string unitAbbreviation,
        bool disposed
    )
    {
        Id = id;
        Name = name;
        ExpirationDateKind = expirationDateKind;
        ExpirationDate = expirationDate;
        CategoryId = categoryId;
        AddedDateTime = addedDateTime;
        CurrentAmount = currentAmount;
        MaxAmount = maxAmount;
        UnitAbbreviation = unitAbbreviation;
        Disposed = disposed;
    }

    public Guid Id { get; set; }
    public string Name { get; set; }
    public ExpirationDateKindDto ExpirationDateKind { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime AddedDateTime { get; set; }
    public double CurrentAmount { get; set; }
    public double MaxAmount { get; set; }
    public string UnitAbbreviation { get; set; }
    public bool Disposed { get; set; }

    public static SimpleProductDto FromDomain(
        Product product,
        LanguageId languageId
    )
    {
        return new SimpleProductDto(
            product.Id,
            product.Name,
            (ExpirationDateKindDto)product.ExpirationDate.Kind,
            product.ExpirationDate.Value,
            product.CategoryId,
            product.AddedDateTime,
            product.Amount.CurrentValue,
            product.Amount.MaxValue,
            product.Amount.Unit.GetAbbreviation(languageId),
            product.DisposedState.Value
        );
    }
}