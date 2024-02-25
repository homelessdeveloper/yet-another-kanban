using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Constants;

namespace Yak.Modules.Kanban.Features.Groups.MoveGroupOver;

// ------------------------------------------------------------ //
// CONTROLLER
// ------------------------------------------------------------ //

/// <summary>
/// Controller for moving a group over another group within a workspace.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Groups)]
[Route(Api.Paths.Workspace.MoveGroupOver)]
[Authorize]
public class MoveGroupOverController(ISender sender) : ControllerBase
{
  /// <summary>
  /// Moves a group over another group within a workspace.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace.</param>
  /// <param name="activeGroupId">The ID of the group to be moved.</param>
  /// <param name="overGroupId">The ID of the group over which the active group will be moved.</param>
  /// <returns>No content if the groups are swapped successfully.</returns>
  [SwaggerOperation(
    OperationId = "MoveGroupOver",
    Summary = "Used to swap the position of two groups within a workspace."
  )]
  [HttpPatch]
  public async Task<ActionResult> MoveGroupOver(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid activeGroupId,
    [FromRoute] Guid overGroupId
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var gAId = GroupId.New(activeGroupId);
    var gOId = GroupId.New(overGroupId);

    var principal = HttpContext.RequirePrincipal();
    await sender.Send(new MoveGroupOverCommand(wId, gAId, gOId, principal));

    return NoContent();
  }
}

