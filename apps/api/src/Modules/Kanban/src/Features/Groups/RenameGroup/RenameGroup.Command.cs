using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.RenameGroup;

/// <summary>
/// Represents a command to rename a group.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace where the group is located.</param>
/// <param name="GroupId">The ID of the group to rename</param>
/// <param name="Name">The new name for the group.</param>
/// <param name="Principal">The principal who initialized the command.</param>
/// <since>0.0.1</since>
public record RenameGroupCommand(
  WorkspaceId WorkspaceId,
  GroupId GroupId,
  GroupName Name,
  Principal Principal
) : ICommand<Unit>;

