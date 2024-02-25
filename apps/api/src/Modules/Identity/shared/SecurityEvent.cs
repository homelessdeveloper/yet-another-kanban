using Yak.Core.Abstractions;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents a collection of security-related domain events.
/// </summary>
/// <since>0.0.1</since>
public static class SecurityEvent
{
  /// <summary>
  /// Represents a domain event indicating that a user has been authenticated.
  /// </summary>
  /// <param name="UserId">Gets the ID of the authenticated user.</param>
  /// <param name="UserName">Gets the username of the authenticated user.</param>
  /// <param name="Email">Gets the email of the authenticated user.</param>
  public record UserAuthenticated(
    UserId UserId,
    UserName UserName,
    Email Email
  ) : IDomainEvent;

  /// <summary>
  /// Represents a domain event indicating that a new user has been registered.
  /// </summary>
  /// <param name="UserId">Gets the ID of the registered user</param>
  /// <param name="UserName">Gets the username of the registered user</param>
  /// <param name="Email">Gets the username of the registered user</param>
  public record UserRegistered(
    UserId UserId,
    UserName UserName,
    Email Email
  ) : IDomainEvent;
}


