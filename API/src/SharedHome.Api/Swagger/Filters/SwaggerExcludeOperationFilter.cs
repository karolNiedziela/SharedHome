using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedHome.Api.Swagger.Filters
{
    public class SwaggerExcludeOperationFilter : IOperationFilter
    {
        private List<string> _excludedProperties = new List<string> { "PersonId" };

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation is null || context is null || context.ApiDescription?.ParameterDescriptions is null)
            {
                return;
            }

            var properties = context.ApiDescription.ParameterDescriptions.FirstOrDefault()
             ?.ModelMetadata?.ContainerType
             ?.GetProperties();

            if (properties is null || properties.Length == 0)
            {
                return;
            }

            foreach (var excludedProperty in _excludedProperties)
            {
                var parameter = operation.Parameters.FirstOrDefault(p => string.Equals(p.Name, excludedProperty, StringComparison.Ordinal));
                operation.Parameters.Remove(parameter);
            }
        }
    }
}
