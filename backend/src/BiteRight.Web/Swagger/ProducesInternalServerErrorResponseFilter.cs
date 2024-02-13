// # ==============================================================================
// # Solution: BiteRight
// # File: ProducesInternalServerErrorResponseFilter.cs
// # Author: Łukasz Sobczak
// # Created: 12-02-2024
// # ==============================================================================

#region

using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

#endregion

namespace BiteRight.Web.Swagger;

public class ProducesInternalServerErrorResponseFilter : IOperationFilter
{
    public void Apply(
        OpenApiOperation operation,
        OperationFilterContext context
    )
    {
        var producesInternalServerErrorResponse = new OpenApiResponse
        {
            Description = "Internal Server Error",
            Content = new Dictionary<string, OpenApiMediaType>
            {
                {
                    "application/json", new OpenApiMediaType
                    {
                        Schema = new OpenApiSchema
                        {
                            Type = "object",
                            Properties = new Dictionary<string, OpenApiSchema>
                            {
                                {
                                    "message", new OpenApiSchema
                                    {
                                        Type = "string"
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        operation.Responses.Add("500", producesInternalServerErrorResponse);
    }
}