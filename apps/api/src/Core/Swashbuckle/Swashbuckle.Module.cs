using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Yak.Core.Swashbuckle;

/// <summary>
/// Provides extension methods for configuring Swagger/OpenAPI with Swashbuckle.
/// </summary>
/// <since>0.0.1</since>
public static class Extensions
{
  /// <summary>
  /// Adds Swashbuckle module to the specified <see cref="IServiceCollection"/>.
  /// </summary>
  /// <param name="services">The service collection to which Swashbuckle module will be added.</param>
  /// <since>0.0.1</since>
  public static void AddSwashbuckleModule(this IServiceCollection services)
  {
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
      c.EnableAnnotations();
      c.SchemaFilter<RequireNonNullablePropertiesSchemaFilter>();
      c.SupportNonNullableReferenceTypes(); // Sets Nullable flags appropriately.
      c.UseAllOfToExtendReferenceSchemas(); // Allows $ref enums to be nullable
      c.UseAllOfForInheritance(); // Allows $ref objects to be nullable
      c.SwaggerDoc("v1", new OpenApiInfo
      {
        Version = "v1",
        Title = "Yet Another Kanban",
        Description = "Yet another kanban board API"
      });

      // Add security definition for JWT Bearer scheme
      c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
      {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description =
          "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
      });

      // Add security requirement for JWT Bearer scheme
      c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    });
  }
}
