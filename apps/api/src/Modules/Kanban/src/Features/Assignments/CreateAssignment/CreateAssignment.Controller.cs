using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Constants;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.CreateAssignment;

/// <summary>
/// Controller for handling the creation of assignments.
/// </summary>
/// <since>0.0.1</since>
[ApiController]
[Tags(Api.Tags.Assignments)]
[Route(Api.Paths.Workspace.CreateAssignment)]
public class CreateAssignmentController(ISender sender) : ControllerBase
{

  /// <summary>
  /// Creates a new assignment inside the specified group.
  /// </summary>
  /// <param name="workspaceId">The ID of the workspace containing the group.</param>
  /// <param name="groupId">The ID of the group where the assignment will be created.</param>
  /// <param name="request">The request containing assignment details.</param>
  /// <returns>The response containing the ID of the created assignment.</returns>
  [SwaggerOperation(OperationId = "CreateAssignment",
    Summary = "Creates new assignment inside specified group."
  )]
  [HttpPost]
  public async Task<ActionResult<CreateAssignmentResponse>> MakeAssignment(
    [FromRoute] Guid workspaceId,
    [FromBody] CreateAssignmentRequest request
  )
  {
    var wId = WorkspaceId.New(workspaceId);
    var gId = GroupId.New(request.GroupId);

    var principal = HttpContext.RequirePrincipal();

    var assignmentId = await sender.Send(new CreateAssignmentCommand(
      wId,
      gId,
      principal,
      AssignmentTitle.New(request.Title),
      request.Description
    ));

    return new CreateAssignmentResponse(assignmentId);
  }
}


