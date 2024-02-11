using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BiteRight.Domain.Languages;
using BiteRight.Domain.Units;

namespace BiteRight.Domain.Abstracts.Repositories;

public interface IUnitRepository
{
    Task<(IEnumerable<Unit> Units, int TotalCount)> Search(
        string query,
        int pageNumber,
        int pageSize,
        LanguageId languageId,
        CancellationToken cancellationToken = default
    );

    Task<Unit?> FindById(
        UnitId id,
        LanguageId languageId,
        CancellationToken cancellationToken
    );
}