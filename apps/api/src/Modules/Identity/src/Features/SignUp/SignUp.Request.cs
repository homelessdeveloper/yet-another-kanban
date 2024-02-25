namespace Yak.Modules.Identity.Features.SignUp;

/// <summary>
/// Represents a sign-up request with email, username, and password.
/// </summary>
/// <param name="Email">The email of the user signing up.</param>
/// <param name="UserName">The username of the user signing up.</param>
/// <param name="Password">The password of the user signing up</param>
/// <since>0.0.1</since>
public record SignUpRequest(
  string Email,
  string UserName,
  string Password
);
