using BiteRight.Application.Dtos.Products;
using MediatR;

namespace BiteRight.Application.Commands.Products.Create;

public record CreateRequest(
    string Name,
    string Description,
    decimal? Price,
    Guid? CurrencyId,
    DateOnly? ExpirationDate,
    ExpirationDateKindDto ExpirationDateKind,
    Guid CategoryId,
    double MaximumAmountValue,
    Guid AmountUnitId
) : IRequest<CreateResponse>;