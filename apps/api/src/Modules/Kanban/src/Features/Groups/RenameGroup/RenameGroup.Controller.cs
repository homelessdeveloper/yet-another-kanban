using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Yak.Modules.Kanban.Constants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Yak.Core.Common;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Features.Groups.RenameGroup;

/// <summary>
/// Controller for renaming a group.
/// </summary>
[ApiController]
[Tags(Api.Tags.Groups)]
[Route(Api.Paths.Workspace.RenameGroup)]
[Authorize]
public class RenameGroupController(ISender sender) : ControllerBase
{
  /// <summary>
  /// Renames a specified group.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the group.</param>
  /// <param name="groupId">The ID of the group to rename.</param>
  /// <param name="request">The request containing the new name for the group.</param>
  /// <returns>No content if the group is renamed successfully.</returns>
  [SwaggerOperation(
    OperationId = "RenameGroup",
    Summary = "Rename specified group"
  )]
  [HttpPut]
  public async Task<ActionResult> RenameGroup(
    [FromRoute] Guid workspaceId,
    [FromRoute] Guid groupId,
    [FromBody] RenameGroupRequest request
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var gId = GroupId.New(groupId);
    var name = GroupName.Validate(request.Name).OrThrow();
    var principal = HttpContext.RequirePrincipal();

    await sender.Send(
      new RenameGroupCommand(
        wId,
        gId,
        name,
        principal
      )
    );

    return NoContent();
  }
}

