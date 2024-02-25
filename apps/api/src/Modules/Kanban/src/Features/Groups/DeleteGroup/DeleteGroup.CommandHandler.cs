using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.DeleteGroup;

/// <summary>
/// Command handler for deleting a group from a workspace.
/// </summary>
/// <since>0.0.1</since>
public class DeleteGroupCommandHandler(IWorkspaceStore workspaces, ILogger<DeleteGroupCommand> logger) : ICommandHandler<DeleteGroupCommand, Unit>
{
  /// <summary>
  /// Handles the delete group command.
  /// </summary>
  /// <param name="request">The delete group command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(DeleteGroupCommand request, CancellationToken cancellationToken)
  {
    // Retrieve the workspace using the provided workspace store.
    var workspace = await workspaces.Get(request.WorkspaceId);

    // If the workspace does not exist, throw a WorkspaceNotFoundById error.
    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Get the group that will be deleted.
    var group = workspace.GetGroup(request.GroupId);

    // Check if the requester is the owner of the workspace.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Delete the group from the workspace.
    workspace.DeleteGroup(request.GroupId);

    // Save the changes to the workspace store.
    await workspaces.SaveChanges();

    // Log useful information about performed action.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
      has deleted group '{group.Name}'
      form workspace '{workspace.Name}'"
    );

    // Return a completed task.
    return Unit.Value;
  }
}


