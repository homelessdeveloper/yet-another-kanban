using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.DeleteGroup;

/// <summary>
/// Represents a command to delete a group from a workspace.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace from which the group should be deleted.</param>
/// <param name="GroupId">The principal executing the command.</param>
/// <param name="Principal">The principal executing the command.</param>
/// <since>0.0.1</since>
public record DeleteGroupCommand(
  WorkspaceId WorkspaceId,
  GroupId GroupId,
  Principal Principal
) : ICommand<Unit>;


