using MediatR;
using LanguageExt;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Core.Common;
using Yak.Modules.Identity.Constants;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.SignUp;

/// <summary>
/// Controller for signing up new users.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Auth)]
[Route(Api.Paths.Auth)]
public class SignUpController
{
  private readonly ISender _sender;
  private readonly ITokenService _tokenService;

  /// <summary>
  /// Initializes a new instance of the <see cref="SignUpController"/> class.
  /// </summary>
  /// <param name="sender">The sender.</param>
  /// <param name="tokenService">The token service.</param>
  public SignUpController(ISender sender, ITokenService tokenService)
  {
    _sender = sender;
    _tokenService = tokenService;
  }

  /// <summary>
  /// Registers and authenticates a new user.
  /// </summary>
  /// <param name="request">The sign-up request.</param>
  /// <returns>An action result containing the authentication response.</returns>
  [SwaggerOperation(
    OperationId = "SignUp",
    Summary = "Registers and authenticates new user"
  )]
  [SwaggerResponse(200, "", typeof(AuthResponse))]
  [HttpPost("SignUp")]
  public async Task<ActionResult<AuthResponse>> SignUp(SignUpRequest request)
  {
    // Validate and process sign-up request
    var args = (
        Email.Validate(request.Email),
        UserName.Validate(request.UserName),
        Password.Validate(request.Password)
      )
      .Apply((email, username, password) => (email, username, password))
      .OrThrow();

    // Send sign-up command
    var principal = await _sender.Send(
      new SignUpCommand(
        Email: args.email,
        UserName: args.username,
        Password: args.password
      )
    );

    // Generate access token
    var accessToken = _tokenService.CreateToken(principal);

    // Return authentication response
    return new AuthResponse(
      Username: principal.UserName.Value,
      Email: principal.Email.Value,
      Token: accessToken
    );
  }
}


