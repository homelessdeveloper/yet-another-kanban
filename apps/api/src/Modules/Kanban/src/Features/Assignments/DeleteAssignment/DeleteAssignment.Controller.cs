using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.DeleteAssignment;

/// <summary>
/// Controller for handling deletion of assignments.
/// </summary>
/// <remarks>
/// This controller provides endpoints for deleting assignments within workspaces.
/// </remarks>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Assignments)]
[Route(Api.Paths.Workspace.DeleteAssignment)]
public class DeleteAssignmentController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Deletes the specified assignment.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
  /// <param name="assignmentId">The ID of the assignment to delete.</param>
  /// <returns>An action result representing the outcome of the operation.</returns>
  [SwaggerOperation(
    OperationId = "DeleteAssignment",
    Summary = "Deletes specified assignment"
  )]
  [HttpDelete]
  public async Task<ActionResult> DeleteAssignment(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid assignmentId
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var aId = AssignmentId.New(assignmentId);
    var principal = HttpContext.RequirePrincipal();

    await sender.Send(new DeleteAssignmentCommand(wId, aId, principal));

    return NoContent();
  }
}

