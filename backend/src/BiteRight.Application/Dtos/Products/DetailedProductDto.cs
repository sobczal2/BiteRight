// # ==============================================================================
// # Solution: BiteRight
// # File: DetailedProductDto.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

using System;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Currencies;
using BiteRight.Application.Dtos.Units;
using BiteRight.Application.Dtos.Users;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Products;

namespace BiteRight.Application.Dtos.Products;

public class DetailedProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double? PriceValue { get; set; }
    public CurrencyDto? PriceCurrency { get; set; }
    public ExpirationDateKindDto ExpirationDateKind { get; set; }
    public DateOnly? ExpirationDateValue { get; set; }
    public CategoryDto Category { get; set; }
    public DateTime AddedDateTime { get; set; }
    public double AmountCurrentValue { get; set; }
    public double AmountMaxValue { get; set; }
    public UnitDto Unit { get; set; }
    public bool DisposedStateValue { get; set; }
    public DateTime? DisposedStateDateTime { get; set; }

    public DetailedProductDto(
        string name,
        string description,
        double? priceValue,
        CurrencyDto? priceCurrency,
        ExpirationDateKindDto expirationDateKind,
        DateOnly? expirationDateValue,
        CategoryDto categoryDto,
        DateTime addedDateTime,
        double amountCurrentValue,
        double amountMaxValue,
        UnitDto unit,
        bool disposedStateValue,
        DateTime? disposedStateDateTime
    )
    {
        Name = name;
        Description = description;
        PriceValue = priceValue;
        PriceCurrency = priceCurrency;
        ExpirationDateKind = expirationDateKind;
        ExpirationDateValue = expirationDateValue;
        Category = categoryDto;
        AddedDateTime = addedDateTime;
        AmountCurrentValue = amountCurrentValue;
        AmountMaxValue = amountMaxValue;
        Unit = unit;
        DisposedStateValue = disposedStateValue;
        DisposedStateDateTime = disposedStateDateTime;
    }

    public static DetailedProductDto FromDomain(
        Product product,
        LanguageId languageId
    )
    {
        return new DetailedProductDto(
            product.Name,
            product.Description,
            product.Price?.Value,
            product.Price is null ? null : CurrencyDto.FromDomain(product.Price.Currency),
            (ExpirationDateKindDto) product.ExpirationDate.Kind,
            product.ExpirationDate.Value,
            CategoryDto.FromDomain(product.Category, languageId),
            product.AddedDateTime,
            product.Amount.CurrentValue,
            product.Amount.MaxValue,
            UnitDto.FromDomain(product.Amount.Unit, languageId),
            product.DisposedState.Value,
            product.DisposedState.DateTime
        );
    }
}