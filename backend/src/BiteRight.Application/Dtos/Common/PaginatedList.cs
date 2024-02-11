namespace BiteRight.Application.Dtos.Common;

public class PaginatedList<T>
{
    public PaginatedList(int pageNumber, int pageSize, int totalCount, IEnumerable<T> items)
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