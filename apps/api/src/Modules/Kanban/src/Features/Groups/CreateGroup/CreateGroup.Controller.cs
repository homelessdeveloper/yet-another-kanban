using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Constants;

namespace Yak.Modules.Kanban.Features.Groups.CreateGroup;


/// <summary>
/// Controller for handling requests related to group creation.
/// </summary>
[ApiController]
[Tags(Api.Tags.Groups)]
[Route(Api.Paths.Workspace.CreateGroup)]
[Authorize]
public class CreateGroupController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Creates a new group inside a workspace.
  /// </summary>
  /// <param name="workspaceId">The unique identifier of the workspace where the group will be created.</param>
  /// <param name="request">The request containing the name of the group to create.</param>
  /// <returns>A response containing the unique identifier of the newly created group.</returns>
  [SwaggerOperation(
    OperationId = "CreateGroup",
    Summary = "Creates new group inside workspace"
  )]
  [HttpPost]
  public async Task<ActionResult<CreateGroupResponse>> AddGroupToWorkspace(
    [FromRoute] Guid workspaceId,
    [FromBody] CreateGroupRequest request
  )
  {
    var id = WorkspaceId.New(workspaceId);
    var groupName = GroupName.New(request.Name);
    var principal = HttpContext.RequirePrincipal();
    var groupId = await sender.Send(new CreateGroupCommand(id, groupName, principal));

    return new CreateGroupResponse(groupId);
  }
}
