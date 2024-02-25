using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using MediatR;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentTo;

/// <summary>
/// Controller for moving assignments to a group within a workspace.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Assignments)]
[Route(Api.Paths.Workspace.MoveAssignmentTo)]
public class MoveAssignmentToController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Moves an assignment to a group within the workspace.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
  /// <param name="assignmentId">The ID of the assignment to be moved.</param>
  /// <param name="groupId">The ID of the group to which the assignment should be moved.</param>
  /// <returns>No content if successful.</returns>
  [SwaggerOperation(
    OperationId = "MoveAssignmentTo",
    Summary = "Used to change the position of assignments within the group."
  )]
  [HttpPut]
  public async Task<ActionResult> MoveAssignmentOver(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid assignmentId,
    [FromRoute] Guid groupId
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var aAId = AssignmentId.New(assignmentId);
    var gId = GroupId.New(groupId);

    var principal = HttpContext.RequirePrincipal();

    await sender.Send(new MoveAssignmentToCommand(wId, aAId, principal, gId));

    return NoContent();
  }
}
