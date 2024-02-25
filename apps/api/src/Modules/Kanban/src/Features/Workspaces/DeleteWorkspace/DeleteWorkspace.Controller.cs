using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.DeleteWorkspace;

/// <summary>
/// Controller for deleting a workspace.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Workspaces)]
[Route(Api.Paths.Workspace.DeleteWorkspace)]
[Authorize]
public class DeleteWorkspaceController(ISender sender) : ControllerBase
{
  /// <summary>
  /// Deletes an existing workspace and all groups and tasks within it.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace to delete.</param>
  /// <returns>No content if the workspace is deleted successfully.</returns>
  [SwaggerOperation(
    OperationId = "DeleteWorkspace",
    Summary = "Deletes existing workspace",
    Description = "Deletes existing workspace and all groups and tasks within it."
  )]
  [SwaggerResponse(204, "")]
  [HttpDelete]
  public async Task<ActionResult> DeleteWorkspace([FromRoute] Guid workspaceId)
  {
    var id = WorkspaceId.New(workspaceId);
    var principal = HttpContext.RequirePrincipal();
    await sender.Send(new DeleteWorkspaceCommand(id, principal));

    return Ok();
  }
}



