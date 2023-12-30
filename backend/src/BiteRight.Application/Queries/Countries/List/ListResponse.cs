using BiteRight.Application.Dtos.Countries;

namespace BiteRight.Application.Queries.Countries.List;

public class ListResponse
{
    public IEnumerable<CountryDto> Countries { get; }
    
    public ListResponse(IEnumerable<CountryDto> countries)
    {
        Countries = countries;
    }
}