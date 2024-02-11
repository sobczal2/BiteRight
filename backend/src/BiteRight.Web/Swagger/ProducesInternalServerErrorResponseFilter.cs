using System.Collections.Generic;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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