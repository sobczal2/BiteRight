// # ==============================================================================
// # Solution: BiteRight
// # File: IProductRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Products;

#endregion

namespace BiteRight.Domain.Abstracts.Repositories;

public interface IProductRepository
{
    void Add(
        Product product
    );

    Task<Product?> FindById(
        ProductId productId,
        CancellationToken cancellationToken = default
    );

    void Delete(
        Product product,
        CancellationToken cancellationToken
    );
}