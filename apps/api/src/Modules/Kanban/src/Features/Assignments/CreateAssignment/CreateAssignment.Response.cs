
namespace Yak.Modules.Kanban.Features.Assignments.CreateAssignment;

/// <summary>
/// Represents the response after making an assignment.
/// </summary>
/// <param name="Id">The id of the created assignment</param>
/// <since>0.0.1</since>
public record CreateAssignmentResponse(Guid Id);
