using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.CreateGroup;

/// <summary>
/// Represents a command to create a new group within a workspace.
/// </summary>
/// <param name="WorkspaceId">The unique identifier of the workspace where group will be created.</param>
/// <param name="Name">The name of the group to be created.</param>
/// <param name="Principal">The principal that creates the group. Must be an owner of the workspace.</param>
/// <since>0.0.1</since>
public record CreateGroupCommand(
  WorkspaceId WorkspaceId,
  GroupName Name,
  Principal Principal
) : ICommand<GroupId>;

