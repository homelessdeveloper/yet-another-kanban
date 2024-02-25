namespace Yak.Modules.Identity.Features.SignIn;

/// <summary>
/// Represents a sign-in request containing email and password.
/// </summary>
/// <param name="Email">The email associated with the sign-in request.</param>
/// <param name="Password">The password associated with the sign-in request.</param>
/// <since>0.0.1</since>
public record SignInRequest(
  string Email,
  string Password
);
