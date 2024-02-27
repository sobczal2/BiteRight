// # ==============================================================================
// # Solution: BiteRight
// # File: ISortingStrategyHandler.cs
// # Author: Łukasz Sobczak
// # Created: 16-02-2024
// # ==============================================================================

#region

using System.Linq;

#endregion

namespace BiteRight.Application.Dtos.Common;

public interface ISortingStrategyHandler<in TStrategy, TEntity>
{
    public IOrderedQueryable<TEntity> Apply(
        IQueryable<TEntity> query,
        TStrategy strategy
    );
}