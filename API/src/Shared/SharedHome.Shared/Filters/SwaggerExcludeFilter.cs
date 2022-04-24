using Microsoft.OpenApi.Models;
using SharedHome.Shared.Abstractions.Attributes;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace SharedHome.Shared.Filters
{
    public class SwaggerExcludeFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation is null || context is null || context.ApiDescription?.ParameterDescriptions is null)
            {
                return;
            }

            var excludedProperties = context.ApiDescription.ParameterDescriptions.FirstOrDefault()
                ?.ModelMetadata?.ContainerType
                ?.GetProperties()
                .Where(property => property.GetCustomAttribute<SwaggerExcludeAttribute>() != null)
                .ToList();

            if (excludedProperties is null || excludedProperties.Count == 0)
            {
                return;
            }

            foreach (var excludedProperty in excludedProperties)
            {
                var parameter = operation.Parameters.FirstOrDefault(p => string.Equals(p.Name, excludedProperty.Name, StringComparison.Ordinal));
                operation.Parameters.Remove(parameter);
            }
        }
    }
}
