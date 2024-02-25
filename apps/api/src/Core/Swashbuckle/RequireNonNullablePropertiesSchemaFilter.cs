using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yak.Core.Swashbuckle;

/// <summary>
/// Implements a schema filter to ensure that non-nullable properties are marked as required in the OpenAPI schema.
/// </summary>
/// <since>0.0.1</since>
public class RequireNonNullablePropertiesSchemaFilter : ISchemaFilter
{
  /// <summary>
  /// Adds non-nullable properties to the list of required properties in the OpenAPI schema.
  /// </summary>
  /// <param name="model">The OpenAPI schema model being modified.</param>
  /// <param name="context">The context information for the schema filter.</param>
  /// <since>0.0.1</since>
  public void Apply(OpenApiSchema model, SchemaFilterContext context)
  {
    // Identify non-nullable properties and add them to the list of required properties
    var additionalRequiredProps = model.Properties
      .Where(x => !x.Value.Nullable && !model.Required.Contains(x.Key))
      .Select(x => x.Key);

    foreach (var propKey in additionalRequiredProps)
    {
      model.Required.Add(propKey);
    }
  }
}
