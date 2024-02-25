using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentOver;

/// <summary>
/// Represents a command to move an assignment over another assignment.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the assignments belong.</param>
/// <param name="Principal">The principal performing the move operation.</param>
/// <param name="ActiveAssignmentId">The ID of the assignment that is currently active.</param>
/// <param name="OverAssignmentId">The ID of the assignment over which the active assignment will be moved.</param>
/// <since>0.0.1</since>
public record MoveAssignmentOverCommand(
  WorkspaceId WorkspaceId,
  Principal Principal,
  AssignmentId ActiveAssignmentId,
  AssignmentId OverAssignmentId
) : ICommand<Unit>;

