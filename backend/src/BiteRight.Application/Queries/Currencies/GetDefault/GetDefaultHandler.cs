// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultHandler.cs
// # Author: Łukasz Sobczak
// # Created: 29-02-2024
// # ==============================================================================

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Currencies;
using BiteRight.Domain.Abstracts.Repositories;

namespace BiteRight.Application.Queries.Currencies.GetDefault;

public class GetDefaultHandler : QueryHandlerBase<GetDefaultRequest, GetDefaultResponse>
{
    private readonly ICurrencyRepository _currencyRepository;

    public GetDefaultHandler(
        ICurrencyRepository currencyRepository
    )
    {
        _currencyRepository = currencyRepository;
    }

    protected override async Task<GetDefaultResponse> HandleImpl(
        GetDefaultRequest request,
        CancellationToken cancellationToken
    )
    {
        var currency = await _currencyRepository.GetDefault(cancellationToken);

        return new GetDefaultResponse(CurrencyDto.FromDomain(currency));
    }
}