// # ==============================================================================
// # Solution: BiteRight
// # File: ListHandler.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Countries;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

#endregion

namespace BiteRight.Application.Queries.Countries.List;

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