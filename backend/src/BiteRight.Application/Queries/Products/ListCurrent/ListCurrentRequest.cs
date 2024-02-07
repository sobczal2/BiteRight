using BiteRight.Application.Dtos.Products;
using MediatR;

namespace BiteRight.Application.Queries.Products.ListCurrent;

public class ListCurrentRequest : IRequest<ListCurrentResponse>
{
    public ProductSortingStrategy SortingStrategy { get; set; }
}