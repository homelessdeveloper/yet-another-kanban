
using System.ComponentModel.DataAnnotations;

namespace Yak.Modules.Identity.Infrastructure;

/// <summary>
/// Represents the settings for JWT (JSON Web Token) authentication.
/// </summary>
/// <since>0.0.1</since>
public class JwtSettings
{
  /// <summary>
  /// The issuer of the JWT.
  /// </summary>
  [Required]
  public string Issuer { get; init; } = null!;

  /// <summary>
  /// The audience of the JWT.
  /// </summary>
  [Required]
  public string Audience { get; init; } = null!;

  /// <summary>
  /// The secret key used to sign the JWT.
  /// </summary>
  [Required]
  public string Secret { get; init; } = null!;
}

