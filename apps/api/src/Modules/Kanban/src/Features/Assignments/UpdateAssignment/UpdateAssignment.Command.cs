using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.UpdateAssignment;

/// <summary>
/// Command to update an assignment's details.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the assignment is located.</param>
/// <param name="Principal">The principal initiating the update operation.</param>
/// <param name="AssignmentId">The ID of the assignment to be updated.</param>
/// <param name="Title">The new title for the assignment (optional).</param>
/// <param name="Description">The new description for the assignment (optional).</param>
public record UpdateAssignmentCommand(
  WorkspaceId WorkspaceId,
  Principal Principal,
  AssignmentId AssignmentId,
  AssignmentTitle? Title,
  string? Description
) : ICommand<Unit>;

