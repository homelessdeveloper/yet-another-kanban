using LanguageExt;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Core.Common;
using Yak.Modules.Identity.Constants;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignIn;


/// <summary>
/// Represents a controller for authenticating existing users.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Auth)]
[Route(Api.Paths.Auth)]
public class SignInController
{
  private readonly ITokenService _tokenService;
  private readonly ISender _sender;

  /// <summary>
  /// Initializes a new instance of the <see cref="SignInController"/> class.
  /// </summary>
  /// <param name="tokenService">The token service used for creating authentication tokens.</param>
  /// <param name="sender">The sender used for sending sign-in commands.</param>
  public SignInController(ITokenService tokenService, ISender sender)
  {
    _tokenService = tokenService;
    _sender = sender;
  }

  /// <summary>
  /// Authenticates an existing user.
  /// </summary>
  /// <param name="request">The sign-in request containing email and password.</param>
  /// <returns>An action result containing the authentication response.</returns>
  [SwaggerOperation(
    OperationId = "SignIn",
    Summary = "Authenticates existing user."
  )]
  [SwaggerResponse(200, "", typeof(AuthResponse))]
  [HttpPost("SignIn")]
  public async Task<ActionResult<AuthResponse>> SignIn(SignInRequest request)
  {
    var args = (
        Email.Validate(request.Email),
        Password.Validate(request.Password)
      )
      .Apply((email, password) => (email, password))
      .OrThrow();

    var principal = await _sender.Send(
      new SignInCommand(
        args.email,
        args.password
      )
    );
    var accessToken = _tokenService.CreateToken(principal);

    return new AuthResponse(
      Username: principal.UserName.Value,
      Email: principal.Email.Value,
      Token: accessToken
    );
  }
}

