using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SharedHome.Shared.Filters
{
    public class SwaggerExcludeFilter : ISchemaFilter
    {
        private List<string> _excludedProperties = new List<string> { "personId" };

        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (schema?.Properties == null || schema.Properties.Count == 0)
            {
                return;
            }

            foreach (var excludedProperty in _excludedProperties)
            {
                if (schema.Properties.ContainsKey(excludedProperty))
                {
                    schema.Properties.Remove(excludedProperty);
                }
            }
        }
    }
}
