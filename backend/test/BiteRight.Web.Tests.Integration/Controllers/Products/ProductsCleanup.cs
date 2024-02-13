// # ==============================================================================
// # Solution: BiteRight
// # File: ProductsCleanup.cs
// # Author: Łukasz Sobczak
// # Created: 13-02-2024
// # ==============================================================================

using System.Threading.Tasks;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Web.Tests.Integration.Controllers.Products;

public static class ProductsCleanup
{
    public static async Task Execute(AppDbContext dbContext)
    {
        await dbContext
            .Products
            .ExecuteDeleteAsync();
    }
}