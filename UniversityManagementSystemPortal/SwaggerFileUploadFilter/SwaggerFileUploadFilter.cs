using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace UniversityManagementSystemPortal
{
    public class FileUploadOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var parameters = operation.Parameters;

            foreach (var parameter in parameters)
            {
                var fileParameter = context.ApiDescription.ParameterDescriptions.FirstOrDefault(x => x.ModelMetadata?.ModelType == typeof(IFormFile));

                if (fileParameter != null && parameter.Name == fileParameter.Name)
                {
                    parameter.Content = new Dictionary<string, OpenApiMediaType>
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "string",
                                Format = "binary"
                            }
                        }
                    };
                }
            }
        }
    }
}
