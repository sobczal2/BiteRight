// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrentHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Common.Exceptions;
using BiteRight.Application.Dtos.Products;
using BiteRight.Application.Queries.Products.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BiteRight.Application.Queries.Products.ListCurrent;

public class ListCurrentHandler : QueryHandlerBase<ListCurrentRequest, ListCurrentResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly IIdentityProvider _identityProvider;

    public ListCurrentHandler(
        IIdentityProvider identityProvider,
        AppDbContext appDbContext
    )
    {
        _identityProvider = identityProvider;
        _appDbContext = appDbContext;
    }

    protected override async Task<ListCurrentResponse> HandleImpl(
        ListCurrentRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var baseQuery =
            _appDbContext
                .Products
                .AsNoTracking()
                .Where(product =>
                    product.UserId == user.Id
                )
                .Where(product =>
                    !product.DisposedState.Disposed
                );

        var sortingStrategyHandler = new ProductSortingSortingStrategyHandler();

        baseQuery = sortingStrategyHandler.Apply(baseQuery, request.SortingStrategy)
            .ThenBy(product => product.Id);

        var products = await baseQuery
            .Select(product => SimpleProductDto.FromDomain(product))
            .ToListAsync(cancellationToken);

        return new ListCurrentResponse(products);
    }
}