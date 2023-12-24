using BiteRight.Application.Dtos.Currencies;
using BiteRight.Domain.Currency;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Currencies.List;

public class ListHandler : IRequestHandler<ListRequest, ListResponse>
{
    private readonly AppDbContext _appDbContext;

    public ListHandler(
        AppDbContext appDbContext
    )
    {
        _appDbContext = appDbContext;
    }

    public async Task<ListResponse> Handle(
        ListRequest request,
        CancellationToken cancellationToken
    )
    {
        var currencyDtos = await _appDbContext
            .Database
            .SqlQueryRaw<CurrencyDto>(
                "SELECT Code, Name, Symbol FROM currency.currencies"
            )
            .ToListAsync(cancellationToken);
        
        return new ListResponse(currencyDtos);
    }
}