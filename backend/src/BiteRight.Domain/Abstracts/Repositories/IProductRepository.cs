using BiteRight.Domain.Products;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface IProductRepository
{
    void Add(Product product);
}