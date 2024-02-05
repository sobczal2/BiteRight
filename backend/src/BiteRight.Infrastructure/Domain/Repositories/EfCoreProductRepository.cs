using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Infrastructure.Database;

namespace BiteRight.Infrastructure.Domain.Repositories;

public class EfCoreProductRepository : IProductRepository
{
    private readonly AppDbContext _appDbContext;

    public EfCoreProductRepository(
        AppDbContext appDbContext
        )
    {
        _appDbContext = appDbContext;
    }
    public void Add(
        Product product
    )
    {
        _appDbContext.Products.Add(product);
    }
}