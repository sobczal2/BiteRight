using BiteRight.Application.Dtos.Countries;
using BiteRight.Application.Dtos.Languages;
using BiteRight.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Languages.List;

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
        var languages = await _appDbContext
            .Database
            .SqlQuery<LanguageDto>(
                $"""
                 SELECT
                    id,
                    name,
                    code
                 FROM language.languages
                 """
            )
            .ToListAsync(cancellationToken);

        return new ListResponse(languages);
    }
}