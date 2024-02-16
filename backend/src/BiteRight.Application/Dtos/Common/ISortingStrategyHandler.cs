// # ==============================================================================
// # Solution: BiteRight
// # File: ISortingStrategyHandler.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

using System.Linq;

namespace BiteRight.Application.Queries.Common;

public interface ISortingStrategyHandler<in TStrategy, TEntity>
{
    public IOrderedQueryable<TEntity> Apply(
        IQueryable<TEntity> query,
        TStrategy strategy
    );
}