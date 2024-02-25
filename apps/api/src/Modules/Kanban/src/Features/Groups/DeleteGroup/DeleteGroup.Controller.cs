using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.DeleteGroup;

/// <summary>
/// Controller for deleting a group.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Groups)]
[Route(Api.Paths.Workspace.DeleteGroup)]
[Authorize]
public class DeleteGroupController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Deletes a specified group.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the group.</param>
  /// <param name="groupId">The ID of the group to delete.</param>
  /// <returns>No content if the group is deleted successfully.</returns>
  [SwaggerOperation(
    OperationId = "DeleteGroup",
    Summary = "Deletes specified group"
  )]
  [HttpDelete]
  public async Task<ActionResult> DeleteGroup(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid groupId
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var gId = GroupId.New(groupId);

    var principal = HttpContext.RequirePrincipal();

    await sender.Send(new DeleteGroupCommand(wId, gId, principal));

    return NoContent();
  }
}
