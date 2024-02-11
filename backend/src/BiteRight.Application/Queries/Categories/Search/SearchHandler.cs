using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchHandler : QueryHandlerBase<SearchRequest, SearchResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILanguageProvider _languageProvider;

    public SearchHandler(
        ILanguageProvider languageProvider,
        ICategoryRepository categoryRepository
    )
    {
        _languageProvider = languageProvider;
        _categoryRepository = categoryRepository;
    }

    protected override async Task<SearchResponse> HandleImpl(
        SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);
        var searchResult = await _categoryRepository.Search(
            request.Query,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            languageId,
            cancellationToken
        );

        var pagedList = new PaginatedList<CategoryDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            searchResult.TotalCount,
            searchResult.Categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.GetName(languageId)
            })
        );

        return new SearchResponse(pagedList);
    }
}