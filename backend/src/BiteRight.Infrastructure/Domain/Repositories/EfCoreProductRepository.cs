// # ==============================================================================
// # Solution: BiteRight
// # File: EfCoreProductRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

#endregion

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

    public async Task<Product?> FindById(
        ProductId productId,
        CancellationToken cancellationToken = default
    )
    {
        return await _appDbContext
            .Products
            .Include(product => product.Amount)
            .Include(product => product.Price)
            .FirstOrDefaultAsync(product => product.Id == productId, cancellationToken);
    }

    public void Delete(
        Product product,
        CancellationToken cancellationToken
    )
    {
        _appDbContext.Products.Remove(product);
    }
}