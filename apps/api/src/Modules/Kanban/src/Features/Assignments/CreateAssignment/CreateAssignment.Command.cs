using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.CreateAssignment;

/// <summary>
/// Represents a command to make an assignment.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where assignment will be made.</param>
/// <param name="GroupId">The ID of the group to which the assignment will be assigned.</param>
/// <param name="Principal">The principal making the assignment.</param>
/// <param name="AssignmentTitle">The title of the assignment.</param>
/// <param name="Description">The description of the assignment.</param>
/// <since>0.0.1</since>
public record CreateAssignmentCommand(
  WorkspaceId WorkspaceId,
  GroupId GroupId,
  Principal Principal,
  AssignmentTitle AssignmentTitle,
  string? Description
) : ICommand<AssignmentId>;
