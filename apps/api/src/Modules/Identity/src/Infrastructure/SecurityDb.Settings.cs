
using System.ComponentModel.DataAnnotations;

namespace Yak.Modules.Identity.Infrastructure;

/// <summary>
/// Represents the settings for the security database.
/// </summary>
/// <since>0.0.1</since>
public class SecurityDbSettings
{
  /// <summary>
  /// The connection string for the security database.
  /// </summary>
  [Required]
  public string ConnectionString { get; init; }
}
