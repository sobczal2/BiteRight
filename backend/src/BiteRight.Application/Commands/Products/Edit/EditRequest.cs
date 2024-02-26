// # ==============================================================================
// # Solution: BiteRight
// # File: EditRequest.cs
// # Author: Łukasz Sobczak
// # Created: 26-02-2024
// # ==============================================================================

using System;
using BiteRight.Application.Dtos.Products;
using MediatR;
using Newtonsoft.Json;

namespace BiteRight.Application.Commands.Products.Edit;

public class EditRequest : IRequest<EditResponse>
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double? PriceValue { get; set; }
    public Guid? PriceCurrencyId { get; set; }
    public ExpirationDateKindDto ExpirationDateKind { get; set; }
    public DateOnly? ExpirationDate { get; set; }
    public Guid CategoryId { get; set; }
    public double AmountCurrentValue { get; set; }
    public double AmountMaxValue { get; set; }
    public Guid AmountUnitId { get; set; }

    public EditRequest(
        Guid id,
        string name,
        string description,
        double? priceValue,
        Guid? priceCurrencyId,
        ExpirationDateKindDto expirationDateKind,
        DateOnly? expirationDate,
        Guid categoryId,
        double amountCurrentValue,
        double amountMaxValue,
        Guid amountUnitId
    )
    {
        Id = id;
        Name = name;
        Description = description;
        PriceValue = priceValue;
        PriceCurrencyId = priceCurrencyId;
        ExpirationDateKind = expirationDateKind;
        ExpirationDate = expirationDate;
        CategoryId = categoryId;
        AmountCurrentValue = amountCurrentValue;
        AmountMaxValue = amountMaxValue;
        AmountUnitId = amountUnitId;
    }
}