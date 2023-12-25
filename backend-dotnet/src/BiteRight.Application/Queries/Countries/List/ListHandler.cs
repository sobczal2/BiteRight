using BiteRight.Application.Dtos.Countries;
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
            .Database
            .SqlQuery<CountryDto>(
                $"""
                 SELECT
                    id,
                    native_name,
                    english_name,
                    alpha2code,
                    official_language_id
                 FROM country.countries
                 """
            )
            .ToListAsync(cancellationToken);

        return new ListResponse(countries);
    }
}