// # ==============================================================================
// # Solution: BiteRight
// # File: GetDefaultHandler.cs
// # Author: Łukasz Sobczak
// # Created: 19-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Application.Dtos.Categories;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;

#endregion

namespace BiteRight.Application.Queries.Categories.GetDefault;

public class GetDefaultHandler : QueryHandlerBase<GetDefaultRequest, GetDefaultResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILanguageProvider _languageProvider;

    public GetDefaultHandler(
        ICategoryRepository categoryRepository,
        ILanguageProvider languageProvider
    )
    {
        _categoryRepository = categoryRepository;
        _languageProvider = languageProvider;
    }

    protected override async Task<GetDefaultResponse> HandleImpl(
        GetDefaultRequest request,
        CancellationToken cancellationToken
    )
    {
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);
        var category = await _categoryRepository.GetDefault(languageId, cancellationToken);

        return new GetDefaultResponse(CategoryDto.FromDomain(category, languageId));
    }
}