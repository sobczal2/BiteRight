// # ==============================================================================
// # Solution: BiteRight
// # File: RestoreHandler.cs
// # Author: Łukasz Sobczak
// # Created: 15-02-2024
// # ==============================================================================

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Restore;

public class RestoreHandler : CommandHandlerBase<RestoreRequest, RestoreResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityProvider _identityProvider;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productLocalizer;
    private readonly IProductRepository _productRepository;

    public RestoreHandler(
        AppDbContext appDbContext,
        IDateTimeProvider dateTimeProvider,
        IIdentityProvider identityProvider,
        IStringLocalizer<Resources.Resources.Products.Products> productLocalizer,
        IProductRepository productRepository
    )
        : base(appDbContext)
    {
        _dateTimeProvider = dateTimeProvider;
        _identityProvider = identityProvider;
        _productLocalizer = productLocalizer;
        _productRepository = productRepository;
    }

    protected override async Task<RestoreResponse> HandleImpl(
        RestoreRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var product = await _productRepository.FindById(request.ProductId, cancellationToken);

        if (product is null || !Equals(product.CreatedById, user.Id))
            throw ValidationException(nameof(RestoreRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );

        if (!product.IsDisposed())
            throw ValidationException(nameof(RestoreRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_not_disposed)]
            );

        product.Restore();

        return new RestoreResponse();
    }
}