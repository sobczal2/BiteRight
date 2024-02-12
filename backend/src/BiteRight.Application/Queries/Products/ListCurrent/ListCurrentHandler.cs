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
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityProvider _identityProvider;
    private readonly IUserRepository _userRepository;

    public ListCurrentHandler(
        IIdentityProvider identityProvider,
        AppDbContext appDbContext,
        IDateTimeProvider dateTimeProvider,
        IUserRepository userRepository
    )
    {
        _identityProvider = identityProvider;
        _appDbContext = appDbContext;
        _dateTimeProvider = dateTimeProvider;
        _userRepository = userRepository;
    }

    protected override async Task<ListCurrentResponse> HandleImpl(
        ListCurrentRequest request,
        CancellationToken cancellationToken
    )
    {
        var identityId = _identityProvider.RequireCurrent();
        var user = await _userRepository.FindByIdentityId(identityId, cancellationToken);

        if (user is null) throw new InternalErrorException();

        var currentLocalDate = _dateTimeProvider.GetLocalDate(user.Profile.TimeZone);


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

        var sortedQuery = ApplySortingStrategy(baseQuery, request.SortingStrategy)
            .ThenBy(product => product.Id);

        var products = await sortedQuery
            .Select(product => new SimpleProductDto
            {
                Id = product.Id,
                Name = product.Name,
                ExpirationDate = product.ExpirationDate.GetDateIfKnown(),
                CategoryId = product.CategoryId,
                AddedDateTime = product.AddedDateTime,
                AmountPercentage = product.Amount.GetPercentage(),
                Disposed = product.DisposedState.Disposed
            })
            .ToListAsync(cancellationToken);

        return new ListCurrentResponse(products);
    }

    private static IOrderedQueryable<Product> ApplySortingStrategy(
        IQueryable<Product> baseQuery,
        ProductSortingStrategy sortingStrategy
    )
    {
        return sortingStrategy switch
        {
            ProductSortingStrategy.NameAsc => baseQuery.OrderBy(product => product.Name),
            ProductSortingStrategy.NameDesc => baseQuery.OrderByDescending(product => product.Name),
            ProductSortingStrategy.ExpirationDateAsc => baseQuery.OrderBy(product => product.ExpirationDate.Value),
            ProductSortingStrategy.ExpirationDateDesc => baseQuery.OrderByDescending(product =>
                product.ExpirationDate.Value),
            ProductSortingStrategy.AddedDateTimeAsc => baseQuery.OrderBy(product => product.Amount),
            ProductSortingStrategy.AddedDateTimeDesc => baseQuery.OrderByDescending(product => product.Amount),
            ProductSortingStrategy.ConsumptionAsc => baseQuery.OrderBy(product => product.Name),
            ProductSortingStrategy.ConsumptionDesc => baseQuery.OrderByDescending(product => product.Name),
            _ => throw new ArgumentOutOfRangeException(nameof(sortingStrategy), sortingStrategy, null)
        };
    }
}