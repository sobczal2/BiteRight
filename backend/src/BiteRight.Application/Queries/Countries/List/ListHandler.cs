using BiteRight.Application.Dtos.Countries;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Countries.List;

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
        var countries = await _appDbContext
            .Countries
            .Select(country => new CountryDto
            {
                Id = country.Id,
                NativeName = country.NativeName,
                EnglishName = country.EnglishName,
                Alpha2Code = country.Alpha2Code,
                OfficialLanguageId = country.OfficialLanguageId,
                CurrencyId = country.CurrencyId
            })
            .ToListAsync(cancellationToken);

        return new ListResponse(countries);
    }
}