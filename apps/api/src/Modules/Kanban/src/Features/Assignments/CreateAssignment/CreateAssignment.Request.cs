
namespace Yak.Modules.Kanban.Features.Assignments.CreateAssignment;

/// <summary>
/// Represents the request to make an assignment.
/// </summary>
/// <param name="Title">The title of the assignment</param>
/// <param name="Description">The description of the assignment</param>
/// <since>0.0.1</since>
public record CreateAssignmentRequest(
  string Title,
  Guid GroupId,
  string? Description
);
