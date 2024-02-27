// # ==============================================================================
// # Solution: BiteRight
// # File: DeleteHandler.cs
// # Author: Łukasz Sobczak
// # Created: 27-02-2024
// # ==============================================================================

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Infrastructure.Database;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.Delete;

public class DeleteHandler : CommandHandlerBase<DeleteRequest, DeleteResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly IProductRepository _productRepository;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productLocalizer;
    private readonly IDateTimeProvider _dateTimeProvider;

    public DeleteHandler(
        AppDbContext appDbContext,
        IIdentityProvider identityProvider,
        IProductRepository productRepository,
        IStringLocalizer<Resources.Resources.Products.Products> productLocalizer,
        IDateTimeProvider dateTimeProvider
    )
        : base(appDbContext)
    {
        _identityProvider = identityProvider;
        _productRepository = productRepository;
        _productLocalizer = productLocalizer;
        _dateTimeProvider = dateTimeProvider;
    }

    protected override async Task<DeleteResponse> HandleImpl(
        DeleteRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var product = await _productRepository.FindById(request.ProductId, cancellationToken);

        if (product is null || !Equals(product.CreatedById, user.Id))
            throw ValidationException(nameof(DeleteRequest.ProductId),
                _productLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]
            );
        
        _productRepository.Delete(product, cancellationToken);
        
        return new DeleteResponse();
    }
}