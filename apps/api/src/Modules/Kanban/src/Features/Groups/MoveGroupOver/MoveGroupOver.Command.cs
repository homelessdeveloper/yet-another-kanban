using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;


namespace Yak.Modules.Kanban.Features.Groups.MoveGroupOver;

/// <summary>
/// Represents a command to move a group over another group within a workspace.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the groups are located.</param>
/// <param name="ActiveGroupId">The ID of the group that will be moved.</param>
/// <param name="OverGroupId">The ID of the group over which the active group will be moved.</param>
/// <param name="Principal">The principal who initiated the command.</param>
/// <since>0.0.1</since>
public record MoveGroupOverCommand(
  WorkspaceId WorkspaceId,
  GroupId ActiveGroupId,
  GroupId OverGroupId,
  Principal Principal
) : ICommand<Unit>;

