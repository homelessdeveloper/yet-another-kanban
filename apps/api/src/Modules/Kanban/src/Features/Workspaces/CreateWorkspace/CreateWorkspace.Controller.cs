using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;
using Yak.Core.Common;

namespace Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;


/// <summary>
/// Controller for creating a new workspace.
/// </summary>
[ApiController]
[Tags(Api.Tags.Workspaces)]
[Route(Api.Paths.Workspace.CreateWorkspace)]
[Authorize]
public class CreateWorkspaceController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Creates a new workspace.
  /// </summary>
  /// <param name="request">The request containing the name of the new workspace.</param>
  /// <returns>The response containing the ID of the new workspace.</returns>
  [SwaggerOperation(
    OperationId = "CreateWorkspace",
    Summary = "Creates new workspace"
  )]
  [SwaggerResponse(200, "", typeof(CreateWorkspaceResponse))]
  [HttpPost]
  public async Task<CreateWorkspaceResponse> CreateWorkspace([FromBody] CreateWorkspaceRequest request)
  {
    var name = WorkspaceName.Validate(request.Name).OrThrow();
    var principal = HttpContext.RequirePrincipal();
    var id = await sender.Send(new CreateWorkspaceCommand(name, principal));

    return new CreateWorkspaceResponse(id);
  }
}

