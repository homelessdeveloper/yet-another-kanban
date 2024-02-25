
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Yak.Core.Abstractions;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Infrastructure;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignUp;

/// <summary>
/// Handles the command for signing up a new user.
/// </summary>
/// <param name="DomainEventPublisher">The domain event publisher used to publish events.</param>
/// <param name="TokenService">The token service.</param>
/// <param name="SecurityDbContext">The security database context.</param>
/// <since>0.0.1</since>
public record SignUpCommandHandler(
  IDomainEventPublisher DomainEventPublisher,
  ITokenService TokenService,
  ILogger<SignUpCommandHandler> Logger,
  UserManager<SecurityUser> UserManager,
  SecurityDbContext SecurityDbContext
) : ICommandHandler<SignUpCommand, Principal>
{
  /// <summary>
  /// Handles the sign-up command asynchronously.
  /// </summary>
  /// <param name="request">The sign-up command request.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task that represents the asynchronous operation. The task result contains the principal of the signed-up user.</returns>
  public async Task<Principal> Handle(SignUpCommand request, CancellationToken cancellationToken)
  {
    // Create a new user
    var user = SecurityUser.Make(
      username: request.UserName,
      email: request.Email,
      refreshToken: null,
      refreshTokenExpiryTime: DateTime.MinValue
    );

    // Attempt to register new user
    var result = await UserManager.CreateAsync(user, request.Password.Value);

    // If there is an error, map it to the domain specific error and throw
    if (!result.Succeeded)
    {
      foreach (var error in result.Errors)
      {
        throw error.Code switch
        {
          "DuplicateEmail" => new SecurityError.DuplicateEmail(request.Email),
          "DuplicateUserName" => new SecurityError.DuplicateUserName(request.UserName),
          // TODO: This exception is too general. Might need a better type.
          _ => new Exception($"{error.Code}, {error.Description}")
        };
      }
    }

    // Log useful information.
    Logger.LogInformation($"User: '{user.Id}' has been registered");

    // Publish the event.
    // This event can be used for email verification,
    // user activity tracking etc.
    await DomainEventPublisher.Publish(
      new SecurityEvent.UserRegistered(
        UserId: user.Id,
        UserName: request.UserName,
        Email: request.Email
      ),
      cancellationToken
    );

    // Return the application-wide representation
    // of the currently authenticated user.
    return new Principal(
      Id: PrincipalId.New(user.Id),
      Email: Email.New(user.Email!),
      UserName: UserName.New(user.UserName!)
    );
  }
}

