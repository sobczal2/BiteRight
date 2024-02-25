// # ==============================================================================
// # Solution: BiteRight
// # File: ChangeAmountHandler.cs
// # Author: Łukasz Sobczak
// # Created: 17-02-2024
// # ==============================================================================

using System;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Commands.Products.Create;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Products.Exceptions;
using BiteRight.Infrastructure.Database;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;

namespace BiteRight.Application.Commands.Products.ChangeAmount;

public class ChangeAmountHandler : CommandHandlerBase<ChangeAmountRequest, ChangeAmountResponse>
{
    private readonly IIdentityProvider _identityProvider;
    private readonly IProductRepository _productRepository;
    private readonly IStringLocalizer<Resources.Resources.Products.Products> _productsLocalizer;

    public ChangeAmountHandler(
        IIdentityProvider identityProvider,
        IProductRepository productRepository,
        IStringLocalizer<Resources.Resources.Products.Products> productsLocalizer,
        AppDbContext appDbContext
    )
        : base(appDbContext)
    {
        _identityProvider = identityProvider;
        _productRepository = productRepository;
        _productsLocalizer = productsLocalizer;
    }

    protected override async Task<ChangeAmountResponse> HandleImpl(
        ChangeAmountRequest request,
        CancellationToken cancellationToken
    )
    {
        var user = await _identityProvider.RequireCurrentUser(cancellationToken);

        var product = await _productRepository.FindById(request.ProductId, cancellationToken)
                      ?? throw ValidationException(nameof(request.ProductId),
                          _productsLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]);

        if (!Equals(product.CreatedById, user.Id))
        {
            throw ValidationException(nameof(request.ProductId),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.product_not_found)]);
        }

        product.Amount.ChangeCurrent(request.Amount);

        return new ChangeAmountResponse();
    }

    protected override ValidationException MapExceptionToValidationException(
        Exception exception
    )
    {
        return exception switch
        {
            AmountCurrentValueInvalidValueException => ValidationException(
                nameof(CreateRequest.MaximumAmountValue),
                _productsLocalizer[nameof(Resources.Resources.Products.Products.amount_current_value_less_than_zero)]
            ),
            AmountCurrentValueGreaterThanMaxValueException => ValidationException(
                nameof(CreateRequest.MaximumAmountValue),
                _productsLocalizer[
                    nameof(Resources.Resources.Products.Products.amount_current_value_greater_than_max_value)]
            ),

            _ => base.MapExceptionToValidationException(exception)
        };
    }
}