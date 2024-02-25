using LanguageExt;

namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents the identifier for a principal.
/// </summary>
public class PrincipalId : NewType<PrincipalId, Guid>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="PrincipalId"/> class with the specified ID.
  /// </summary>
  /// <param name="id">The ID of the principal.</param>
  /// <since>0.0.1</since>
  public PrincipalId(Guid id) : base(id)
  {
  }

  /// <summary>
  /// Generates a new random principal ID.
  /// </summary>
  /// <returns>A new random principal ID.</returns>
  public static PrincipalId Random() => New(Guid.NewGuid());

  /// <summary>
  /// Implicitly converts a <see cref="PrincipalId"/> object to a <see cref="UserId"/> object.
  /// </summary>
  /// <param name="id">The principal ID to be converted.</param>
  /// <returns>A <see cref="UserId"/> object equivalent to the provided principal ID.</returns>
  public static implicit operator UserId(PrincipalId id) => UserId.New(id);

  /// <summary>
  /// Implicitly converts a <see cref="PrincipalId"/> object to a <see cref="Guid"/>.
  /// </summary>
  /// <param name="id">The principal ID to be converted.</param>
  /// <returns>The underlying <see cref="Guid"/> value of the principal ID.</returns>
  public static implicit operator Guid(PrincipalId id) => id.Value;
}
