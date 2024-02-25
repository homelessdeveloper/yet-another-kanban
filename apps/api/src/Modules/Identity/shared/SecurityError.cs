using Yak.Core.Common;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents a collection of security-related errors.
/// </summary>
public static class SecurityError
{

  /// <summary>
  /// Represents an error indicating that the current user is not authetnicated
  /// </summary>
  public class AuthenticationError : ApplicationError
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCredentials"/> class.
    /// </summary>
    public AuthenticationError() : base(401, "User is not authenticated.")
    {
    }
  }


  /// <summary>
  /// Represents an error indicating that a user was not found by email.
  /// </summary>
  /// <since>0.0.1</since>
  public class UserNotFoundByEmail : ApplicationError
  {
    /// <summary>
    /// Gets the email associated with the error.
    /// </summary>
    public Email Email { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UserNotFoundByEmail"/> class with the specified email.
    /// </summary>
    /// <param name="email">The email associated with the error.</param>
    public UserNotFoundByEmail(Email email) : base($"Could not find user with email: {email}", 404)
    {
      Email = email;
    }
  }

  /// <summary>
  /// Represents an error indicating that a user with a duplicate email was found.
  /// </summary>
  public class DuplicateEmail : ApplicationError
  {
    /// <summary>
    /// Gets the email associated with the error.
    /// </summary>
    public Email Email { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateEmail"/> class with the specified email.
    /// </summary>
    /// <param name="email">The email associated with the error.</param>
    public DuplicateEmail(Email email) : base(
      409,
      $"User with email: {email} has been already registered"
    )
    {
      Email = email;
    }
  }

  /// <summary>
  /// Represents an error indicating that a user with a duplicate username was found.
  /// </summary>
  public class DuplicateUserName : ApplicationError
  {
    /// <summary>
    /// Gets the username associated with the error.
    /// </summary>
    public UserName UserName { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="DuplicateUserName"/> class with the specified username.
    /// </summary>
    /// <param name="userName">The username associated with the error.</param>
    public DuplicateUserName(UserName userName) : base(
      409,
      $"User with username: {userName} has been already registered"
    )
    {
      UserName = userName;
    }
  }

  /// <summary>
  /// Represents an error indicating that the provided credentials are not valid.
  /// </summary>
  public class InvalidCredentials : ApplicationError
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidCredentials"/> class.
    /// </summary>
    public InvalidCredentials() : base(403, "Credentials are not valid.")
    {
    }
  }
}
