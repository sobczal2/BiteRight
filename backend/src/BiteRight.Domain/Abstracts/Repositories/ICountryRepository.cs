// # ==============================================================================
// # Solution: BiteRight
// # File: ICountryRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Countries;

#endregion

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