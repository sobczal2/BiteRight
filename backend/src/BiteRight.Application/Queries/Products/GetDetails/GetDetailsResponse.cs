// # ==============================================================================
// # Solution: BiteRight
// # File: GetDetailsResponse.cs
// # Author: Łukasz Sobczak
// # Created: 25-02-2024
// # ==============================================================================

#region

using BiteRight.Application.Dtos.Products;

#endregion

namespace BiteRight.Application.Queries.Products.GetDetails;

public class GetDetailsResponse
{
    public GetDetailsResponse(
        DetailedProductDto product
    )
    {
        Product = product;
    }

    public DetailedProductDto Product { get; set; }
}