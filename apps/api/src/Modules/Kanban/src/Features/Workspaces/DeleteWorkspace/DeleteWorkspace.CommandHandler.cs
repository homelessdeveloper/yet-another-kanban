
using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.DeleteWorkspace;

/// <summary>
/// Command handler for deleting a workspace.
/// </summary>
/// <since>0.0.1</since>
public class DeleteWorkspaceCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<DeleteWorkspaceCommand> logger
) : ICommandHandler<DeleteWorkspaceCommand, Unit>
{
  /// <summary>
  /// Handles the delete workspace command.
  /// </summary>
  /// <param name="request">The delete workspace command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
  {
    // Get the workspace which we are going to delete.
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if we can delete this workspace
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Delete the workspace and publis the events.
    workspaces.Remove(workspace);
    await workspaces.SaveChanges();

    // Log information about performed action.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
      has deleted workspace '{workspace.Name}'"
    );

    return Unit.Value;
  }
}

