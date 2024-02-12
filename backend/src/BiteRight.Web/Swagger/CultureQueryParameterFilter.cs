// # ==============================================================================
// # Solution: BiteRight
// # File: CultureQueryParameterFilter.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace BiteRight.Web.Swagger;

public class CultureQueryParameterFilter : IOperationFilter
{
    public void Apply(
        OpenApiOperation operation,
        OperationFilterContext context
    )
    {
        var cultureQueryParameter = new OpenApiParameter
        {
            Name = "culture",
            In = ParameterLocation.Query,
            Description = "Culture",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        };

        operation.Parameters.Add(cultureQueryParameter);
    }
}