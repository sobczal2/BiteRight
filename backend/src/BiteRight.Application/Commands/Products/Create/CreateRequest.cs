// # ==============================================================================
// # Solution: BiteRight
// # File: CreateRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Application.Dtos.Products;
using MediatR;

#endregion

namespace BiteRight.Application.Commands.Products.Create;

public class CreateRequest : IRequest<CreateResponse>
{
    public CreateRequest(
        string name,
        string description,
        double? priceValue,
        Guid? priceCurrencyId,
        ExpirationDateKindDto expirationDateKind,
        DateOnly? expirationDate,
        Guid categoryId,
        double amountMaxValue,
        Guid amountUnitId
    )
    {
        Name = name;
        Description = description;
        PriceValue = priceValue;
        PriceCurrencyId = priceCurrencyId;
        ExpirationDateKind = expirationDateKind;
        ExpirationDate = expirationDate;
        CategoryId = categoryId;
        AmountMaxValue = amountMaxValue;
        AmountUnitId = amountUnitId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public double? PriceValue { get; set; }
    public Guid? PriceCurrencyId { get; set; }
    public ExpirationDateKindDto ExpirationDateKind { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public double AmountMaxValue { get; set; }
    public Guid AmountUnitId { get; set; }
}