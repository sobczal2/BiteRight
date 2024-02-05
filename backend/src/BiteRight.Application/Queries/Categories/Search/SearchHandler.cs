using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using MediatR;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchHandler : QueryHandlerBase<SearchRequest, SearchResponse>
{
    private readonly ILanguageProvider _languageProvider;
    private readonly ICategoryRepository _categoryRepository;

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
        var categories = await _categoryRepository.Search(
            request.Query,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            languageId,
            cancellationToken
        );

        var pagedList = new PaginatedList<CategoryDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            categories.TotalCount,
            categories.Categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.GetName(languageId),
                PhotoUri = category.GetPhotoUri(),
            })
        );

        return new SearchResponse(pagedList);
    }
}