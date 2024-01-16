using BiteRight.Domain.Countries;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface ICountryRepository
{
    Task<Country?> FindById(
        CountryId id,
        CancellationToken cancellationToken = default
    );
    
    Task<bool> ExistsById(
        CountryId id,
        CancellationToken cancellationToken = default
    );
}