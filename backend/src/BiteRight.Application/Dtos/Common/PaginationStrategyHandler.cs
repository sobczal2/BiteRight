// # ==============================================================================
// # Solution: BiteRight
// # File: PaginationStrategyHandler.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using System.Linq;

namespace BiteRight.Application.Dtos.Common;

public class PaginationStrategyHandler
{
    public IQueryable<TEntity> Apply<TEntity>(
        IQueryable<TEntity> query,
        PaginationParams paginationParams
    )
    {
        return query
            .Skip(paginationParams.PageNumber * paginationParams.PageSize)
            .Take(paginationParams.PageSize);
    }
}