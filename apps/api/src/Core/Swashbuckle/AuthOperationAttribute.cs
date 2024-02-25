using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Yak.Core.Swashbuckle;

/// <summary>
/// Implements an operation filter for adding authentication-related information to Swagger/OpenAPI operations.
/// </summary>
/// <since>0.0.1</since>
public class AuthOperationAttribute : IOperationFilter
{
  /// <summary>
  /// Applies authentication-related modifications to the Swagger/OpenAPI operation.
  /// </summary>
  /// <param name="operation">The operation being modified.</param>
  /// <param name="context">The context information for the operation filter.</param>
  /// <since>0.0.1</since>
  public void Apply(OpenApiOperation operation, OperationFilterContext context)
  {
    var authAttributes = context.MethodInfo
      .GetCustomAttributes(true)
      .OfType<AuthorizeAttribute>()
      .Distinct();

    if (authAttributes.Any())
    {
      if (operation.Parameters == null)
        operation.Parameters = new List<OpenApiParameter>();

      // Add Authorization header parameter
      operation.Parameters.Add(new OpenApiParameter
      {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "JWT access token",
        Required = true,
      });

      // Add responses for unauthorized and forbidden requests
      operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
      operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });

      // Add JWT bearer security scheme
      operation.Security.Add(new OpenApiSecurityRequirement
      {
      {
        new OpenApiSecurityScheme
        {
          Reference = new OpenApiReference
          {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer"
          }
        },
          new string[] { }
      }
      });
    }
  }
}

