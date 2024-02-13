// # ==============================================================================
// # Solution: BiteRight
// # File: ICurrencyRepository.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Currencies;

#endregion

namespace BiteRight.Domain.Abstracts.Repositories;

public interface ICurrencyRepository
{
    Task<Currency?> FindById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    );

    Task<bool> ExistsById(
        CurrencyId id,
        CancellationToken cancellationToken = default
    );
}