
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignIn;

/// <summary>
/// Represents a command for signing in with an email and password.
/// </summary>
/// <param name="Email">The email associated with the sign-in command.</param>
/// <param name="Password">The password associated with the sign-in command.</param>
/// <since>0.0.1</since>
public record SignInCommand(
  Email Email,
  Password Password
) : ICommand<Principal>;
