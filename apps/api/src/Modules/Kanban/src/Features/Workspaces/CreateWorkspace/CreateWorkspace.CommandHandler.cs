using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;

/// <summary>
/// Command handler responsible for handling the <see cref="CreateWorkspaceCommand"/>.
/// </summary>
/// <since>0.0.1</since>
public class CreateWorkspaceCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<CreateWorkspaceCommandHandler> logger
) : ICommandHandler<CreateWorkspaceCommand, WorkspaceId>
{
  /// <summary>
  /// Handles the <see cref="CreateWorkspaceCommand"/> by creating a new workspace.
  /// </summary>
  /// <param name="request">The create workspace command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The ID of the newly created workspace.</returns>
  /// <exception cref="KanbanError.DuplicateWorkspace">Thrown when a workspace with the same name already exists for the user.</exception>
  public async Task<WorkspaceId> Handle(CreateWorkspaceCommand request, CancellationToken cancellationToken)
  {
    // Create the workspace.
    var workspace = Workspace.Create(request.Name, request.Principal.Id);

    // Check if workspace name unique across user's workspaces
    var existingWorkspace = await workspaces.Get(request.Name);

    if (existingWorkspace is not null && existingWorkspace.OwnerId == request.Principal.Id)
      throw new KanbanError.DuplicateWorkspace(request.Name);

    // Persist the workspace and publish the events
    workspaces.Add(workspace);
    await workspaces.SaveChanges();

    // Log information about performed action.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
        has created workspace '{workspace.Name}'"
    );

    return workspace.Id;
  }
}

