// # ==============================================================================
// # Solution: BiteRight
// # File: CreateRequest.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using BiteRight.Application.Dtos.Products;
using MediatR;

#endregion

namespace BiteRight.Application.Commands.Products.Create;

public record CreateRequest(
    string Name,
    string Description,
    decimal? Price,
    Guid? CurrencyId,
    ExpirationDateKindDto ExpirationDateKind,
    DateOnly? ExpirationDate,
    Guid CategoryId,
    double MaximumAmountValue,
    Guid AmountUnitId
) : IRequest<CreateResponse>;