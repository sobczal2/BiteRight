using BiteRight.Application.Dtos.Countries;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Countries.List;

public class ListHandler : IRequestHandler<ListRequest, ListResponse>
{
    private readonly AppDbContext _appDbContext;
    private readonly ILanguageProvider _languageProvider;

    public ListHandler(
        AppDbContext appDbContext,
        ILanguageProvider languageProvider
    )
    {
        _appDbContext = appDbContext;
        _languageProvider = languageProvider;
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
                OfficialLanguageId = country.OfficialLanguageId
            })
            .ToListAsync(cancellationToken);

        return new ListResponse(countries);
    }
}