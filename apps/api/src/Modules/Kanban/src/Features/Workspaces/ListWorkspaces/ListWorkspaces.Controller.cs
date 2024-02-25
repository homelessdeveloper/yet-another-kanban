using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Features.Common;

namespace Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

// ------------------------------------------------------------ //
// CONTROLLER
// ------------------------------------------------------------ //

/// <summary>
/// Controller for listing all workspaces that a user owns.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Workspaces)]
[Route(Api.Paths.Workspace.ListWorksapces)]
[Authorize]
public class ListWorkspacesController(ISender sender) : ControllerBase
{
  /// <summary>
  /// Lists all workspaces that the user owns.
  /// </summary>
  /// <returns>An enumerable collection of <see cref="WorkspaceResponse"/> objects representing the workspaces.</returns>
  [Authorize]
  [SwaggerOperation(
    OperationId = "ListWorkspaces",
    Summary = "Lists all workspaces that user owns"
  )]
  [SwaggerResponse(200, "Returns", typeof(IEnumerable<WorkspaceResponse>))]
  [HttpGet]
  public Task<IEnumerable<ListWorkspacesQueryResult>> ListWorkspaces()
  {
    var principal = HttpContext.RequirePrincipal();
    return sender.Send(new ListWorkspacesQuery(principal));
  }
}

