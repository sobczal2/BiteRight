// # ==============================================================================
// # Solution: BiteRight
// # File: ListCurrentRequest.cs
// # Author: Łukasz Sobczak
// # Created: 11-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Products;
using MediatR;

#endregion

namespace BiteRight.Application.Queries.Products.ListCurrent;

public class ListCurrentRequest : IRequest<ListCurrentResponse>
{
    public ProductSortingStrategy SortingStrategy { get; set; }
}