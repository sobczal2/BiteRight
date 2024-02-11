using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Languages;
using BiteRight.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace BiteRight.Application.Queries.Languages.List;

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
        var languages = await _appDbContext
            .Languages
            .AsNoTracking()
            .Select(language => new LanguageDto
            {
                Id = language.Id,
                Name = language.EnglishName,
                Code = language.Code
            })
            .ToListAsync(cancellationToken);

        return new ListResponse(languages);
    }
}