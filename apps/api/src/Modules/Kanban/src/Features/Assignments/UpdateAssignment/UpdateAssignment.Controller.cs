using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Yak.Modules.Kanban.Constants;

namespace Yak.Modules.Kanban.Features.Assignments.UpdateAssignment;

/// <summary>
/// Controller for updating assignments.
/// </summary>
[ApiController]
[Tags(Api.Tags.Assignments)]
[Route($"{Api.Paths.Workspace.UpdateAssignment}")]
public class UpdateAssignmentController : ControllerBase
{
  /// <summary>
  /// Updates an existing assignment.
  /// </summary>
  /// <param name="assignmentId">The ID of the assignment to update.</param>
  /// <param name="request">The update request containing the new assignment details.</param>
  /// <returns>No content if successful.</returns>
  [SwaggerOperation(
    OperationId = "UpdateAssignment",
    Summary = "Updates existing assignment"
  )]
  [HttpPut]
  public Task<ActionResult> UpdateAssignment(
    [FromRoute] Guid assignmentId,
    [FromBody] UpdateAssignmentRequest request
  )
  {
    // TODO: implement this.
    throw new NotImplementedException();
  }
}


