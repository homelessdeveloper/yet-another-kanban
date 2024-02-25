namespace Yak.Modules.Identity.Features;

/// <summary>
/// Represents an authentication response containing username, email, and token.
/// </summary>
/// <param name="Username">The username associated with the authenticated user.</param>
/// <param name="Email">The email associated with the authenticated user.</param>
/// <param name="Token">The authentication token.</param>
/// <since>0.0.1</since>
public record AuthResponse(
  string Username,
  string Email,
  string Token
);

