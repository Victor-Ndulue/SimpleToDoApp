using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SimpleToDoApp.Extensions.Configs;

public class ODataParameterOperationFilter : IOperationFilter
{

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var queryAttribute = context.MethodInfo.GetCustomAttributes(true)
           .Union(context.MethodInfo.DeclaringType.GetCustomAttributes(true))
           .OfType<EnableQueryAttribute>().FirstOrDefault();

        if (queryAttribute != null)
        {
            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$filter",
                In = ParameterLocation.Query,
                Description = "The $filter OData query option allows clients to filter a collection of resources that are addressed by a request URL",
                Required = false
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$select",
                In = ParameterLocation.Query,
                Description = "The $select OData query option allows clients to specify column names",
                Required = false
            });


            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$orderby",
                In = ParameterLocation.Query,
                Description = "The $orderby OData query option allows clients to specify sort parameters",
                Required = false
            });


            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$count",
                In = ParameterLocation.Query,
                Description = "The $count OData query option allows clients to return row counts",
                Required = false
            });


            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$top",
                In = ParameterLocation.Query,
                Description = "The $top OData query option allows clients to specify pagination page size parameters",
                Required = false
            });


            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$skip",
                In = ParameterLocation.Query,
                Description = "The $skip OData query option allows clients to specify pagination limit parameters",
                Required = false
            });


            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$expand",
                In = ParameterLocation.Query,
                Description = "The $expand OData query option allows clients to expand nested entities",
                Required = false
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$format",
                In = ParameterLocation.Query,
                Description = "The $format OData query option allows clients to specify response formats. Options include json,xml",
                Required = false
            });
        }
    }

}
