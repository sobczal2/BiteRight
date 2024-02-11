using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Product?> FindById(ProductId id, CancellationToken cancellationToken = default)
    {
        return await _appDbContext
            .Products
            .FirstOrDefaultAsync(product => product.Id == id, cancellationToken);
    }
}