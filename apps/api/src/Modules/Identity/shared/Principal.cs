namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents a principal, containing an ID, email, and username.
/// </summary>
/// <param name="Id">The ID of the principal.</param>
/// <param name="Email">The email associated with the principal.</param>
/// <param name="UserName">The username associated with the principal.</param>
/// <since>0.0.1</since>
public record Principal(
  PrincipalId Id,
  Email Email,
  UserName UserName
)
{
  /// <summary>
  /// Implicitly converts a <see cref="Principal"/> object to a <see cref="User"/> object.
  /// </summary>
  /// <param name="principal">The principal to be converted.</param>
  /// <returns>A <see cref="User"/> object equivalent to the provided principal.</returns>
  public static implicit operator User(Principal principal)
  {
    return new User(
      id: principal.Id,
      userName: principal.UserName,
      email: principal.Email
    );
  }
}
