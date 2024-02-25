// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsHandler.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Products;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsHandler : QueryHandlerBase<GetDetailsRequest, GetDetailsResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly ILanguageProvider _languageProvider;
    private readonly AppDbContext _appDbContext;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productsLocalizer;

    public GetDetailsHandler(
        IIdentityProvider identityProvider,
        ILanguageProvider languageProvider,
        AppDbContext appDbContext,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer
    )
    {
        _identityProvider = identityProvider;
        _languageProvider = languageProvider;
        _appDbContext = appDbContext;
        _productsLocalizer = productsLocalizer;
    }

    protected override async Task<GetDetailsResponse> HandleImpl(
        GetDetailsRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);

        var product =
            await _appDbContext
                .Products
                .AsNoTracking()
                .Where(product =>
                    product.Id == request.ProductId
                    && product.CreatedById == user.Id
                )
                .Include(product => product.Amount)
                .ThenInclude(amount => amount.Unit)
                .ThenInclude(unit => unit.Translations.Where(translation => translation.LanguageId == languageId))
                .Include(product => product.Price)
                .ThenInclude(price => price.Currency)
                .Include(product => product.Category)
                .ThenInclude(category =>
                    category.Translations.Where(translation => translation.LanguageId == languageId))
                .SingleOrDefaultAsync(cancellationToken);

        if (product is null)
        {
            throw ValidationException(
                nameof(GetDetailsRequest.ProductId),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );
        }

        return new GetDetailsResponse(DetailedProductDto.FromDomain(product, languageId));
    }
}