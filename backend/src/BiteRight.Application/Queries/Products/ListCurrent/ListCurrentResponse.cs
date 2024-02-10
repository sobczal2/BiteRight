using BiteRight.Application.Dtos.Products;

namespace BiteRight.Application.Queries.Products.ListCurrent;

public record ListCurrentResponse(IEnumerable<SimpleProductDto> Products);