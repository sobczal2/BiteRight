using BiteRight.Domain.Currencies;

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