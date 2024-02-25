
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignUp;

/// <summary>
/// Represents a command for signing up a new user.
/// </summary>
/// <remarks>
/// This command is used to create a new user account with the provided email, username, and password.
/// </remarks>
/// <param name="Email">The email of the user signing up</param>
/// <param name="UserName">The username of the user signing up.</param>
/// <param name="Password">The password of the user signing up.</param>
/// <since>0.0.1</since>
public record SignUpCommand(
  Email Email,
  UserName UserName,
  Password Password
) : ICommand<Principal>;


