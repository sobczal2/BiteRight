// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsResponse.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

using BiteRight.Application.Dtos.Products;

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsResponse
{
    public DetailedProductDto Product { get; set; }

    public GetDetailsResponse(
        DetailedProductDto product
    )
    {
        Product = product;
    }
}