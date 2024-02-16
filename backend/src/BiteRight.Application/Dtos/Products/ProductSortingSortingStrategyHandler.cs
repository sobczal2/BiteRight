// # ==============================================================================
// # Solution: BiteRight
// # File: ProductSortingStrategyHandler.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using System;
using System.Linq;
using BiteRight.Application.Dtos.Products;
using BiteRight.Application.Queries.Common;
using BiteRight.Domain.Products;

namespace BiteRight.Application.Queries.Products.Common;

public class ProductSortingSortingStrategyHandler : ISortingStrategyHandler<ProductSortingStrategy, Product>
{
    public IOrderedQueryable<Product> Apply(
        IQueryable<Product> query,
        ProductSortingStrategy strategy
    )
    {
        return strategy switch
        {
            ProductSortingStrategy.NameAsc => query.OrderBy(product => product.Name),
            ProductSortingStrategy.NameDesc => query.OrderByDescending(product => product.Name),
            ProductSortingStrategy.ExpirationDateAsc => query.OrderBy(product =>
                product.ExpirationDate.Kind == ExpirationDate.ExpirationDateKind.Infinite ? DateOnly.MaxValue
                : product.ExpirationDate.Kind == ExpirationDate.ExpirationDateKind.Unknown ? DateOnly.MinValue
                : product.ExpirationDate.Value
            ),
            ProductSortingStrategy.ExpirationDateDesc => query.OrderByDescending(product =>
                product.ExpirationDate.Kind == ExpirationDate.ExpirationDateKind.Infinite ? DateOnly.MaxValue
                : product.ExpirationDate.Kind == ExpirationDate.ExpirationDateKind.Unknown ? DateOnly.MinValue
                : product.ExpirationDate.Value
            ),
            ProductSortingStrategy.AddedDateTimeAsc => query.OrderBy(product => product.AddedDateTime),
            ProductSortingStrategy.AddedDateTimeDesc => query.OrderByDescending(product => product.AddedDateTime),
            ProductSortingStrategy.PercentageAmountAsc => query.OrderBy(product =>
                product.Amount.CurrentValue / product.Amount.MaxValue),
            ProductSortingStrategy.PercentageAmountDesc => query.OrderByDescending(product =>
                product.Amount.CurrentValue / product.Amount.MaxValue),
            _ => throw new ArgumentOutOfRangeException(nameof(strategy), strategy, null)
        };
    }
}