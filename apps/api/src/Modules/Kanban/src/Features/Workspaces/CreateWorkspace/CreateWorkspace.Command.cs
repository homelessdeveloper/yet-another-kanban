using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;

/// <summary>
/// Represents a command to create a new workspace.
/// </summary>
/// <param name="Name">The name of the new workspace</param>
/// <param name="Principal">The principal who is creating the workspace.</param>
/// <since>0.0.1</since>
public record CreateWorkspaceCommand(
  WorkspaceName Name,
  Principal Principal
) : ICommand<WorkspaceId>;
