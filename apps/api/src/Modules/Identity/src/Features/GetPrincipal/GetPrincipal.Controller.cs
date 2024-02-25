using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Constants;
using Yak.Modules.Identity.Features.Common;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Features.GetPrincipal;

/// <summary>
/// Represents a controller for retrieving the currently authenticated user.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Auth)]
[Route(Api.Paths.Auth)]
[Authorize]
public class GetPrincipalController : ControllerBase
{
  /// <summary>
  /// Retrieves the currently authenticated user.
  /// </summary>
  /// <returns>The principal response containing information about the authenticated user.</returns>
  [SwaggerOperation(
    OperationId = "GetPrincipal",
    Summary = "Retrieves currently authenticated user"
    )]
  [SwaggerResponse(200, "", typeof(PrincipalResponse))]
  [HttpGet("GetPrincipal")]
  public PrincipalResponse GetPrincipal()
  {
    var principal = HttpContext.RequirePrincipal();
    return principal.ToPrincipalResponse();
  }
}
