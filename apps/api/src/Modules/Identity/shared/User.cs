namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents a user entity in the system.
/// </summary>
/// <since>0.0.1</since>
public class User
{
  /// <summary>
  /// Gets the id of the user.
  /// </summary>
  public UserId Id { get; private set; }

  /// <summary>
  /// Gets the email of the user.
  /// </summary>
  public Email Email { get; private set; }

  /// <summary>
  /// Gets the username of the user.
  /// </summary>
  public UserName UserName { get; private set; }


  /// <summary>
  /// This constructor is needed for an ORM only and should stay private.
  /// </summary>
  /// <param name="id">The identifier of the user.</param>
  // ReSharper disable once UnusedMember.Local
  private User(UserId id)
  {
    Id = id;
    UserName = null!;
    Email = null!;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="id">The identifier of the user.</param>
  /// <param name="email">The email of the user.</param>
  /// <param name="userName">The username of the user.</param>
  /// <param name="password">The encrypted password of the user.</param>
  public User(
    UserId id,
    Email email,
    UserName userName
  )
  {
    Id = id;
    Email = email;
    UserName = userName;
  }

  /// <summary>
  /// Creates a new instance of the <see cref="User"/> class.
  /// </summary>
  /// <param name="email">The email of the user.</param>
  /// <param name="userName">The username of the user.</param>
  /// <param name="password">The encrypted password of the user.</param>
  /// <returns>A new instance of the <see cref="User"/> class.</returns>
  /// <remarks>
  ///  This static factory method exists mostly for more robust integration with functional code.
  /// </remarks>
  public static User Make(
    Email email,
    UserName userName
  )
  {
    var id = UserId.Random();
    return new User(
      id,
      email,
      userName
    );
  }
}
