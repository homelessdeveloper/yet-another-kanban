using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.DeleteAssignment;

/// <summary>
/// Represents a command to delete an assignment.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the assignment belongs</param>
/// <param name="AssignmentId">The ID of the assignment to delete</param>
/// <param name="Principal">The principal performing the deletion</param>
/// <since>0.0.1</since>
public record DeleteAssignmentCommand(
  WorkspaceId WorkspaceId,
  AssignmentId AssignmentId,
  Principal Principal
) : ICommand<Unit>;
