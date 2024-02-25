using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentOver;

/// <summary>
/// Controller for moving assignments within a workspace.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Assignments)]
[Route(Api.Paths.Workspace.MoveAssignmentOver)]
public class MoveAssignmentOverController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Moves an assignment over another assignment within the group.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the assignments.</param>
  /// <param name="activeAssignmentId">The ID of the assignment to be moved.</param>
  /// <param name="overAssignmentId">The ID of the assignment over which to move.</param>
  /// <returns>No content if successful.</returns>
  [SwaggerOperation(
    OperationId = "MoveAssignmentOver",
    Summary = "Changes the position of assignments within the group."
  )]
  [HttpPut]
  public async Task<ActionResult> MoveAssignmentOver(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid activeAssignmentId,
    [FromRoute] Guid overAssignmentId
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var aAId = AssignmentId.New(activeAssignmentId);
    var aBId = AssignmentId.New(overAssignmentId);

    var principal = HttpContext.RequirePrincipal();

    await sender.Send(new MoveAssignmentOverCommand(wId, principal, aAId, aBId));

    return NoContent();
  }
}


