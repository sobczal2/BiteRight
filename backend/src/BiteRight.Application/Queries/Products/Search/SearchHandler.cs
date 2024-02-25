// # ==============================================================================
// # Solution: BiteRight
// # File: ListHandler.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Common;
using BiteRight.Application.Dtos.Products;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Products.Search;

public class SearchHandler : QueryHandlerBase<SearchRequest, SearchResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly ILanguageProvider _languageProvider;
    private readonly AppDbContext _appDbContext;
    private readonly IStringLocalizer<Resources.Resources.Categories.Categories> _categoriesLocalizer;

    public SearchHandler(
        IIdentityProvider identityProvider,
        ILanguageProvider languageProvider,
        AppDbContext appDbContext,
        IStringLocalizer<Resources.Resources.Categories.Categories> categoriesLocalizer
    )
    {
        _identityProvider = identityProvider;
        _languageProvider = languageProvider;
        _appDbContext = appDbContext;
        _categoriesLocalizer = categoriesLocalizer;
    }

    protected override async Task<SearchResponse> HandleImpl(
        SearchRequest request,
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
                    product.CreatedById == user.Id);


        var queryToLower = request.Query.ToLower();

        baseQuery = baseQuery
            .Where(product =>
#pragma warning disable CA1862
                ((string)product.Name).ToLower().Contains(queryToLower)
                || ((string)product.Description).ToLower().Contains(queryToLower)
                || product.Category.Translations.Any(translation =>
                        translation.LanguageId == languageId
                        && ((string)translation.Name).ToLower().Contains(queryToLower)
#pragma warning restore CA1862
                )
            );

        if (request.FilteringParams.CategoryIds.Count != 0)
        {
            var allCategoriesExist = _appDbContext
                .Categories
                .Count(category =>
                    request.FilteringParams.CategoryIds.Contains(category.Id)
                ) == request.FilteringParams.CategoryIds.Count;

            if (!allCategoriesExist)
                throw ValidationException(nameof(request.FilteringParams.CategoryIds),
                    _categoriesLocalizer[nameof(Resources.Resources.Categories.Categories.category_not_found)]);

            baseQuery = baseQuery
                .Where(product =>
                    request.FilteringParams.CategoryIds.Contains(product.CategoryId)
                );
        }

        var totalCount = await baseQuery.CountAsync(cancellationToken);

        var sortingStrategyHandler = new ProductSortingSortingStrategyHandler();

        baseQuery = sortingStrategyHandler.Apply(baseQuery, request.SortingStrategy);

        var paginationStrategyHandler = new PaginationStrategyHandler();

        baseQuery = paginationStrategyHandler.Apply(baseQuery, request.PaginationParams);

        var products = await baseQuery
            .Include(product => product.Amount)
            .ThenInclude(amount => amount.Unit)
            .ThenInclude(unit => unit.Translations.Where(translation => translation.LanguageId == languageId))
            .Select(product => SimpleProductDto.FromDomain(product, languageId))
            .ToListAsync(cancellationToken);

        var paginatedList = new PaginatedList<SimpleProductDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            totalCount,
            products
        );

        return new SearchResponse(paginatedList);
    }
}