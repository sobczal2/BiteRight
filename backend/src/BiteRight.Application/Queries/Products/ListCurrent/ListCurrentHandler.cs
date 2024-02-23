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
    private readonly ILanguageProvider _languageProvider;

    public ListCurrentHandler(
        IIdentityProvider identityProvider,
        ILanguageProvider languageProvider,
        AppDbContext appDbContext
    )
    {
        _identityProvider = identityProvider;
        _languageProvider = languageProvider;
        _appDbContext = appDbContext;
    }

    protected override async Task<ListCurrentResponse> HandleImpl(
        ListCurrentRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);

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
            .Include(product => product.Amount)
            .ThenInclude(amount => amount.Unit)
            .ThenInclude(unit => unit.Translations.Where(translation => translation.LanguageId == languageId))
            .Select(product => SimpleProductDto.FromDomain(product, languageId))
            .ToListAsync(cancellationToken);

        return new ListCurrentResponse(products);
    }
}