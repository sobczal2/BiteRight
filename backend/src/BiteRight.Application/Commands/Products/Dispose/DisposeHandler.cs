// # ==============================================================================
// # Solution: BiteRight
// # File: DisposeHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Localization;

#endregion

namespace BiteRight.Application.Commands.Products.Dispose;

public class DisposeHandler : CommandHandlerBase<DisposeRequest, DisposeResponse>
{
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IIdentityProvider _identityProvider;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productLocalizer;
    private readonly IProductRepository _productRepository;

    public DisposeHandler(
        AppDbContext appDbContext,
        IIdentityProvider identityProvider,
        IProductRepository productRepository,
        IStringLocalizer<Resources.Resources.Products.Products> productLocalizer,
        IDateTimeProvider dateTimeProvider
    ) : base(
        appDbContext
    )
    {
        _identityProvider = identityProvider;
        _productRepository = productRepository;
        _productLocalizer = productLocalizer;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task<DisposeResponse> HandleImpl(DisposeRequest request, CancellationToken cancellationToken)
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var product = await _productRepository.FindById(request.ProductId, cancellationToken);

        if (product is null || !Equals(product.CreatedById, user.Id))
            throw ValidationException(nameof(DisposeRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );

        if (product.IsDisposed())
            throw ValidationException(nameof(DisposeRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_already_disposed)]
            );

        product.Dispose(_dateTimeProvider.UtcNow);

        return new DisposeResponse();
    }
}