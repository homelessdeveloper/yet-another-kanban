using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;
using Yak.Core.Common;

namespace Yak.Modules.Kanban.Features.Workspaces.RenameWorkspace;



/// <summary>
/// Controller for renaming a workspace.
/// </summary>
[ApiController]
[Tags(Api.Tags.Workspaces)]
[Route(Api.Paths.Workspace.RenameWorksapce)]
[Authorize]
public class RenameWorkspaceController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Renames a workspace by its ID.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace to rename.</param>
  /// <param name="request">The request containing the new name for the workspace.</param>
  /// <returns>No content if the workspace is renamed successfully.</returns>
  [SwaggerOperation(
    OperationId = "RenameWorkspace",
    Summary = "Rename workspace by id"
  )]
  [SwaggerResponse(204, "Nothing.")]
  [HttpPost()]
  public async Task<ActionResult> RenameWorkspace(
    [FromRoute] Guid workspaceId,
    [FromBody] RenameWorkspaceRequest request
  )
  {
    var id = WorkspaceId.New(workspaceId);
    var name = WorkspaceName.Validate(request.Name).OrThrow();
    var principal = HttpContext.RequirePrincipal();

    await sender.Send(new RenameWorkspaceCommand(id, name, principal));
    return NoContent();
  }
}

