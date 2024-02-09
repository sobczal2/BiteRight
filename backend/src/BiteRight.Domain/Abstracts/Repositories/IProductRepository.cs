using BiteRight.Domain.Products;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface IProductRepository
{
    void Add(Product product);

    Task<Product?> FindById(
        ProductId id,
        CancellationToken cancellationToken = default
    );
}