using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Dispose;

public class DisposeHandler : CommandHandlerBase<DisposeRequest>
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

    protected override async Task<Unit> HandleImpl(DisposeRequest request, CancellationToken cancellationToken)
    {
        var user = await _identityProvider.RequireCurrentUser();

        var product = await _productRepository.FindById(request.ProductId, cancellationToken);

        if (product is null || !Equals(product.UserId, user.Id))
            throw ValidationException(nameof(DisposeRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );

        if (product.IsDisposed())
            throw ValidationException(nameof(DisposeRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_already_disposed)]
            );

        product.SetDisposed(_dateTimeProvider.UtcNow);

        return Unit.Value;
    }
}