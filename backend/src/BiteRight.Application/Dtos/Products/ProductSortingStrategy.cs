// # ==============================================================================
// # Solution: BiteRight
// # File: ProductSortingStrategy.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

namespace BiteRight.Application.Dtos.Products;

// TODO na ekranie z listą wszystkich produktów,
// sortowanie, wyszukiwanie(nazwa, kategoria, opisie)
// i filtrowanie(po stanie daty ważnośc - expired, nie expired,
// po kategorii)
public enum ProductSortingStrategy
{
    NameAsc = 0,
    NameDesc = 1,
    ExpirationDateAsc = 2,
    ExpirationDateDesc = 3,
    AddedDateTimeAsc = 4,
    AddedDateTimeDesc = 5,
    PercentageAmountAsc = 6,
    PercentageAmountDesc = 7,
    Default = NameAsc
}