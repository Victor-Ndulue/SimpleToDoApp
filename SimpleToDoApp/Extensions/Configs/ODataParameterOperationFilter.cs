using Microsoft.AspNetCore.OData.Query;
using Microsoft.OpenApi.Any;
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
                Description = "The $filter OData query option allows clients to filter a collection of resources that are addressed by a request URL. Example: `Name eq 'Task1'`",
                Required = false,
                Example = new OpenApiString("title eq 'Task1'")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$select",
                In = ParameterLocation.Query,
                Description = "The $select OData query option allows clients to specify column names. Example: `Name,DueDate`",
                Required = false,
                Example = new OpenApiString("taskID,title")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$orderby",
                In = ParameterLocation.Query,
                Description = "The $orderby OData query option allows clients to specify sort parameters. Example: `DueDate desc`",
                Required = false,
                Example = new OpenApiString("dueDateTime desc")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$count",
                In = ParameterLocation.Query,
                Description = "The $count OData query option allows clients to return row counts. Example: `true`",
                Required = false,
                Example = new OpenApiString("true")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$top",
                In = ParameterLocation.Query,
                Description = "The $top OData query option allows clients to specify pagination page size parameters. Example: `10`",
                Required = false,
                Example = new OpenApiString("10")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$skip",
                In = ParameterLocation.Query,
                Description = "The $skip OData query option allows clients to specify pagination limit parameters. Example: `5`",
                Required = false,
                Example = new OpenApiString("5")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$expand",
                In = ParameterLocation.Query,
                Description = "The $expand OData query option allows clients to expand nested entities. Example: `Tasks,Subtasks`",
                Required = false,
                Example = new OpenApiString("Tasks,Subtasks")
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "$format",
                In = ParameterLocation.Query,
                Description = "The $format OData query option allows clients to specify response formats. Options include json,xml. Example: `json`",
                Required = false,
                Example = new OpenApiString("json")
            });
        }
    }
}
