using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Features.Common;


namespace Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;

/// <summary>
/// Controller for retrieving a workspace by its ID.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Workspaces)]
[Route(Api.Paths.Workspace.GetWorkspaceById)]
[Authorize]
public class GetWorkspaceByIdController(ISender sender) : ControllerBase
{
  /// <summary>
  /// Retrieves a workspace by its ID.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace to retrieve.</param>
  /// <returns>A <see cref="WorkspaceResponse"/> object representing the retrieved workspace.</returns>
  [SwaggerOperation(
    OperationId = "GetWorkspaceById",
    Summary = "Retrieves workspace by its ID",
    Description = "Deletes existing workspace and all groups and tasks within it."
  )]
  [SwaggerResponse(200, "When workspace exists", typeof(WorkspaceResponse))]
  [HttpGet]
  public async Task<WorkspaceResponse> GetWorkspaceById([FromRoute] Guid workspaceId)
  {
    var id = WorkspaceId.New(workspaceId);

    var principal = HttpContext.RequirePrincipal();
    return await sender.Send(new GetWorkspaceByIdQuery(id, principal));
  }
}


