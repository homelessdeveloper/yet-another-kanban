
using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.RenameWorkspace;

/// <summary>
/// Command handler for renaming a workspace.
/// </summary>
/// <since>0.0.1</since>
public class RenameWorkspaceCommandHandler(
    IWorkspaceStore workspaces,
    ILogger<RenameWorkspaceCommandHandler> logger
) : ICommandHandler<RenameWorkspaceCommand, Unit>
{
  /// <summary>
  /// Handles the rename workspace command.
  /// </summary>
  /// <param name="request">The rename workspace command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(RenameWorkspaceCommand request, CancellationToken cancellationToken)
  {

    // Get the workspace that we are going to rename.
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Save the old name of the workspace for the logging purposes.
    var oldName = workspace.Name;

    // Check if user can rename workspace.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Verify that new workspace name is unique across user's workspaces
    var existingWorkspace = await workspaces.Get(request.WorkspaceName);

    if (existingWorkspace is not null && existingWorkspace.OwnerId == request.Principal.Id)
      throw new KanbanError.DuplicateWorkspace(workspace.Name);

    // Rename workspace and persist changes.
    // This will also publish all aggregate events.
    workspace.Rename(request.WorkspaceName);
    await workspaces.SaveChanges();

    // Log useful information about performed action
    logger.LogInformation(
        @$"Principal '{request.Principal.UserName}'
        has renamed workspace '{oldName}' to {request.WorkspaceName}"
    );

    return Unit.Value;
  }
}

