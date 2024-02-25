using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.DeleteWorkspace;

/// <summary>
/// Represents a command to delete a workspace.
/// </summary>
/// <param name="WorkspaceId">The ID of the workspace to delete.</param>
/// <param name="Principal">THe principal who is deleting the workspace.</param>
/// <since>0.0.1</since>
public record DeleteWorkspaceCommand(
  WorkspaceId WorkspaceId,
  Principal Principal
) : ICommand<Unit>;

