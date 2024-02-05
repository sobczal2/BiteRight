using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Currencies;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Currencies.List;

public class ListHandler : QueryHandlerBase<ListRequest, ListResponse>
{
    private readonly AppDbContext _appDbContext;

    public ListHandler(
        AppDbContext appDbContext
        )
    {
        _appDbContext = appDbContext;
    }
    protected override async Task<ListResponse> HandleImpl(
        ListRequest request,
        CancellationToken cancellationToken
    )
    {
        var currencies = await _appDbContext
            .Currencies
            .Select(currency => new CurrencyDto
            {
                Id = currency.Id,
                Name = currency.Name,
                Symbol = currency.Symbol,
                ISO4217Code = currency.ISO4217Code,
            })
            .ToListAsync(cancellationToken);
        
        return new ListResponse(currencies);
    }
}