// # ==============================================================================
// # Solution: BiteRight
// # File: SimpleProductDto.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Domain.Products;

#endregion

namespace BiteRight.Application.Dtos.Products;

public class SimpleProductDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public DateTime AddedDateTime { get; set; }
    public double AmountPercentage { get; set; }
    public bool Disposed { get; set; }
    
    public static SimpleProductDto FromDomain(Product product)
    {
        return new SimpleProductDto
        {
            Id = product.Id,
            Name = product.Name,
            ExpirationDate = product.ExpirationDate.GetDateIfKnown(),
            CategoryId = product.CategoryId,
            AddedDateTime = product.AddedDateTime,
            AmountPercentage = product.Amount.GetPercentage(),
            Disposed = product.DisposedState.Disposed
        };
    }
}