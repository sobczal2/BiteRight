// # ==============================================================================
// # Solution: BiteRight
// # File: PaginationParams.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

namespace BiteRight.Application.Dtos.Common;

public class PaginationParams
{
    public PaginationParams(
        int pageNumber,
        int pageSize
    )
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }

    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    
    public static PaginationParams Default => new(0, 10);
}