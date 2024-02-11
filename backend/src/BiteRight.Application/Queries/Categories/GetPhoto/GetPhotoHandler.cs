using System.Threading;
using System.Threading.Tasks;
using BiteRight.Application.Common;
using BiteRight.Domain.Abstracts.Common;
using BiteRight.Domain.Abstracts.Repositories;
using BiteRight.Domain.Categories;

namespace BiteRight.Application.Queries.Categories.GetPhoto;

public class GetPhotoHandler : QueryHandlerBase<GetPhotoRequest, GetPhotoResponse>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IFileProvider _fileProvider;
    private readonly ILanguageProvider _languageProvider;

    public GetPhotoHandler(
        ICategoryRepository categoryRepository,
        ILanguageProvider languageProvider,
        IFileProvider fileProvider
    )
    {
        _categoryRepository = categoryRepository;
        _languageProvider = languageProvider;
        _fileProvider = fileProvider;
    }

    protected override async Task<GetPhotoResponse> HandleImpl(
        GetPhotoRequest request,
        CancellationToken cancellationToken
    )
    {
        var languageId = await _languageProvider.RequireCurrentId(cancellationToken);

        var category = await _categoryRepository.FindById(
            request.CategoryId,
            languageId,
            cancellationToken
        ) ?? throw ValidationException(
            nameof(request.CategoryId),
            nameof(Resources.Resources.Categories.Categories.category_not_found)
        );

        var photoName = category.GetPhotoName();
        const string directory = Photo.Directory;
        var fileStream = _fileProvider.GetStream(directory, photoName);

        return new GetPhotoResponse
        {
            PhotoStream = fileStream,
            ContentType = Photo.ContentType,
            FileName = photoName
        };
    }
}