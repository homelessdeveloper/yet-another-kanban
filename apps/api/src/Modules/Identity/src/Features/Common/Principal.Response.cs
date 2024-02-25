using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.Common;

/// <summary>
/// Represents the response model for a principal entity.
/// </summary>
/// <param name="Id">Gets the unique identifier of the principal.</param>
/// <param name="Username">Gets the username of the principal.</param>
/// <param name="Email">Gets the email address of the principal.</param>
/// <since>0.0.1</since>
public record PrincipalResponse(
  Guid Id,
  string Username,
  string Email
);

/// <summary>
/// Provides extension methods for converting principal entities to response models.
/// </summary>
/// <since>0.0.1</since>
public static class PrincipalExtensions
{
  /// <summary>
  /// Converts a <see cref="Principal"/> entity to a <see cref="PrincipalResponse"/> response model.
  /// </summary>
  /// <param name="this">The principal entity to convert.</param>
  /// <returns>A <see cref="PrincipalResponse"/> containing information from the principal entity.</returns>
  public static PrincipalResponse ToPrincipalResponse(this Principal @this)
  {
    return new PrincipalResponse(
      Id: @this.Id.Value,
      Email: @this.Email.Value,
      Username: @this.UserName.Value
    );
  }
}
