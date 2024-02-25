using LanguageExt;

namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents the identifier for a user.
/// </summary>
/// <since>0.0.1</since>
public class UserId : NewType<UserId, Guid>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="UserId"/> class with the specified ID.
  /// </summary>
  /// <param name="id">The ID of the user.</param>
  public UserId(Guid id) : base(id)
  {
  }

  /// <summary>
  /// Implicitly converts a <see cref="UserId"/> object to a <see cref="Guid"/>.
  /// </summary>
  /// <param name="id">The user ID to be converted.</param>
  /// <returns>The underlying <see cref="Guid"/> value of the user ID.</returns>
  public static implicit operator Guid(UserId id) => id.Value;

  /// <summary>
  /// Generates a new random user ID.
  /// </summary>
  /// <returns>A new random user ID.</returns>
  public static UserId Random() => New(Guid.NewGuid());
}

