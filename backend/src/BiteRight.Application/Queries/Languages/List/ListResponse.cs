using BiteRight.Application.Dtos.Languages;

namespace BiteRight.Application.Queries.Languages.List;

public class ListResponse
{
    public IEnumerable<LanguageDto> Languages { get; }
    
    public ListResponse(IEnumerable<LanguageDto> languages)
    {
        Languages = languages;
    }
}