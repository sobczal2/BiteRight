// # ==============================================================================
// # Solution: BiteRight
// # File: PaginatedList.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System;
using System.Collections.Generic;

#endregion

namespace BiteRight.Application.Dtos.Common;

public class PaginatedList<T>
{
    public PaginatedList(
        int pageNumber,
        int pageSize,
        int totalCount,
        IEnumerable<T> items
    )
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        TotalCount = totalCount;
        TotalPages = (int)MathF.Ceiling(totalCount / (float)pageSize);
        Items = items;
    }

    public int PageNumber { get; }
    public int PageSize { get; }
    public int TotalCount { get; }
    public int TotalPages { get; }
    public IEnumerable<T> Items { get; }
}