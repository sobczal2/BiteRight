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
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public ExpirationDateKindDto ExpirationDateKind { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime AddedDateTime { get; set; }
    public double CurrentAmount { get; set; }
    public double MaxAmount { get; set; }
    public string UnitAbbreviation { get; set; } = default!;
    public bool Disposed { get; set; }

    public static SimpleProductDto FromDomain(
        Product product,
        LanguageId languageId
    )
    {
        return new SimpleProductDto
        {
            Id = product.Id,
            Name = product.Name,
            ExpirationDateKind = (ExpirationDateKindDto)product.ExpirationDate.Kind,
            ExpirationDate = product.ExpirationDate.GetDateIfKnown(),
            CategoryId = product.CategoryId,
            AddedDateTime = product.AddedDateTime,
            CurrentAmount = product.Amount.CurrentValue,
            MaxAmount = product.Amount.MaxValue,
            UnitAbbreviation = product.Amount.Unit.GetAbbreviation(languageId),
            Disposed = product.DisposedState.Value
        };
    }
}