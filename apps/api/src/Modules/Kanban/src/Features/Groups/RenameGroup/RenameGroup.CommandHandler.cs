using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.RenameGroup;

/// <summary>
/// Command handler responsible for handling the <see cref="RenameGroupCommand"/>.
/// </summary>
/// <since>0.0.1</since>
public class RenameGroupCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<RenameGroupCommandHandler> logger
) : ICommandHandler<RenameGroupCommand, Unit>
{
  /// <summary>
  /// Handles the <see cref="RenameGroupCommand"/> by renaming the group within the workspace.
  /// </summary>
  /// <param name="request">The rename group command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
  /// <exception cref="KanbanError.WorkspaceNotFoundById">Thrown when the workspace with the specified ID is not found.</exception>
  /// <exception cref="KanbanError.WorkspaceOwnership">Thrown when the workspace ownership check fails.</exception>
  public async Task<Unit> Handle(RenameGroupCommand request, CancellationToken cancellationToken)
  {
    // Retrieve the workspace in which we will perform actions.
    var workspace = await workspaces.Get(request.WorkspaceId);


    // Check if workspace exists, if not - throw an appropriate error.
    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if principal (current user performing the action) owns the workspace.
    // If not, throw an appropriate error.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(workspace.Id, request.Principal.Id);

    // Get the group which will be renamed.
    // We need it for the logging purposes.
    var group = workspace.GetGroup(request.GroupId);

    // Use workspace aggregate to move one group over another
    workspace.RenameGroup(request.GroupId, request.Name);

    // Commit changes made to the workspace and publish the events
    await workspaces.SaveChanges();

    // Log information about performed action.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
        has renamed group '{group.Name}'
        in workspace '{workspace.Name}'"
    );

    return Unit.Value;
  }
}

