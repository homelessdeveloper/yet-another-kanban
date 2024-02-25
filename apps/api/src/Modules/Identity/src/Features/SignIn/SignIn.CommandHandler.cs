
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Yak.Core.Abstractions;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Infrastructure;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignIn;

/// <summary>
/// Handles the sign-in command by authenticating the user with the provided credentials.
/// </summary>
/// <param name="UserManager">The user manager responsible for user-related operations.</param>
/// <param name="Context">The database context for security-related operations.</param>
/// <param name="DomainEventPublisher">The domain event publisher for publishing authentication events.</param>
/// <since>0.0.1</since>
public record SignInCommandHandler(
  UserManager<SecurityUser> UserManager,
  SecurityDbContext Context,
  ILogger<SignInCommandHandler> Logger,
  IDomainEventPublisher DomainEventPublisher
) : ICommandHandler<SignInCommand, Principal>
{
  /// <summary>
  /// Handles the sign-in command asynchronously.
  /// </summary>
  /// <param name="request">The sign-in command containing email and password.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The principal of the authenticated user.</returns>
  /// <since>0.0.1</since>
  public async Task<Principal> Handle(SignInCommand request, CancellationToken cancellationToken)
  {
    var (email, password) = request;
    var user = await UserManager.FindByEmailAsync(email.Value);

    if (user is null)
      throw new SecurityError.InvalidCredentials();

    if (await UserManager.CheckPasswordAsync(user, password.Value) is false)
      throw new SecurityError.InvalidCredentials();

    await DomainEventPublisher.Publish(
      new SecurityEvent.UserAuthenticated(
        UserId: user.Id,
        UserName: UserName.New(user.UserName!),
        Email: request.Email
      ), cancellationToken);

    Logger.LogInformation($"User: '{user.Id}' has logged-in");

    return new Principal(
      Id: PrincipalId.New(user.Id),
      Email: Email.New(user.Email!),
      UserName: UserName.New(user.UserName!)
    );
  }
}

