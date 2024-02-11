using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

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