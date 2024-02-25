
namespace Yak.Modules.Kanban.Features.Assignments.UpdateAssignment;

/// <summary>
/// Request object for updating an assignment.
/// </summary>
/// <param name="Title">The new title for the assignment (optional). </param>
/// <param name="Description">The new description for the assignment (optional).</param>
public record UpdateAssignmentRequest(
  string? Title,
  string? Description
);
