using BiteRight.Application.Dtos.Categories;
using BiteRight.Application.Dtos.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using MediatR;

namespace BiteRight.Application.Queries.Categories.Search;

public class SearchHandler : IRequestHandler<SearchRequest, SearchResponse>
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

    public async Task<SearchResponse> Handle(
        SearchRequest request,
        CancellationToken cancellationToken
    )
    {
        var categories = await _categoryRepository.Search(
            request.Query,
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            await _languageProvider.RequireCurrentId(cancellationToken),
            cancellationToken
        );

        var pagedList = new PaginatedList<CategoryDto>(
            request.PaginationParams.PageNumber,
            request.PaginationParams.PageSize,
            categories.TotalCount,
            categories.Categories.Select(category => new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                PhotoUri = category.GetPhotoUri(),
            })
        );

        return new SearchResponse(pagedList);
    }
}