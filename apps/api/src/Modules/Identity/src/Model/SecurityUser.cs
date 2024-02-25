using Microsoft.AspNetCore.Identity;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Model;

/// <summary>
/// Represents a security user in the application.
/// </summary>
/// <since>0.0.1</since>
public class SecurityUser : IdentityUser<UserId>
{
  // ------------------------------------------------------------ //
  // CONSTRUCTORS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Private parameterless constructor for <see cref="SecurityUser"/>.
  /// </summary>
  /// <remarks>
  /// This constructor is private to prevent direct instantiation of <see cref="SecurityUser"/> objects without parameters.
  /// </remarks>
  private SecurityUser()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SecurityUser"/> class with the specified ID, username, and email.
  /// </summary>
  /// <param name="id">The ID of the user.</param>
  /// <param name="username">The username of the user.</param>
  /// <param name="email">The email of the user.</param>
  private SecurityUser(
    UserId id,
    string username,
    string email
  )
  {
    Id = id;
    Email = email;
    UserName = username;
    EmailConfirmed = true;
  }

  // ------------------------------------------------------------ //
  // STATIC CONSTRUCTORS (FACTORIES)
  // ------------------------------------------------------------ //

  /// <summary>
  /// Creates a new <see cref="SecurityUser"/> instance.
  /// </summary>
  /// <param name="email">The email of the user.</param>
  /// <param name="username">The username of the user.</param>
  /// <param name="refreshTokenExpiryTime">The expiry time for the refresh token.</param>
  /// <param name="refreshToken">The refresh token (optional).</param>
  /// <returns>A new <see cref="SecurityUser"/> instance.</returns>
  public static SecurityUser Make(
    Email email,
    UserName username,
    DateTime refreshTokenExpiryTime,
    string? refreshToken = null
  )
  {
    return new SecurityUser(
      id: UserId.Random(),
      username: username.Value,
      email: email.Value
    );
  }
}
