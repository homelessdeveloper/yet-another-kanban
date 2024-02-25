using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.RenameWorkspace;

/// <summary>
/// Represents a command to rename a workspace.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace to rename.</param>
/// <param name="WorkspaceName">The new name for the workspace.</param>
/// <param name="Principal">The principal who is renaming the workspace.</param>
/// <since>0.0.1</since>
public record RenameWorkspaceCommand(
  WorkspaceId WorkspaceId,
  WorkspaceName WorkspaceName,
  Principal Principal
) : ICommand<Unit>;


