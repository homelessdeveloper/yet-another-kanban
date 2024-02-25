using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentTo;

/// <summary>
/// Command to move an assignment to a group within a workspace.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the assignment is located.</param>
/// <param name="AssignmentId">The ID of the assignment to be moved.</param>
/// <param name="Principal">The prencipal initianing the move operation.</param>
/// <param name="GroupId">THe ID of the group to witch the assignment should be moved. </param>
/// <since>0.0.1</since>
public record MoveAssignmentToCommand(
  WorkspaceId WorkspaceId,
  AssignmentId AssignmentId,
  Principal Principal,
  GroupId GroupId
) : ICommand<Unit>;
