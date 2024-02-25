using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.MoveGroupOver;

/// <summary>
/// Command handler for moving a group over another group within a workspace.
/// </summary>
/// <since>0.0.1</since>
public class MoveGroupOverCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<MoveGroupOverCommandHandler> logger
) : ICommandHandler<MoveGroupOverCommand, Unit>
{
  /// <summary>
  /// Handles the move group over command.
  /// </summary>
  /// <param name="request">The move group over command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(MoveGroupOverCommand request, CancellationToken cancellationToken)
  {
    // Retrieve the workspace in which we will perform actions.
    var workspace = await workspaces.Get(request.WorkspaceId);

    // Check if workspace exists, if not - throw appropriate error.
    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if principal (current user performing the action) owns the workspace.
    // If not, throw an appropriate error.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(workspace.Id, request.Principal.Id);

    // Get groups that are participanting in an action
    var activeGroup = workspace.GetGroup(request.ActiveGroupId);
    var overGroup = workspace.GetGroup(request.OverGroupId);

    // Use workspace aggregate to move one group over another
    workspace.MoveGroupOver(request.ActiveGroupId, request.OverGroupId);

    // Commit changes made to the workspace and publish the events
    await workspaces.SaveChanges();

    // Log information about performed action.
    logger.LogInformation(
      $@"Principal '{request.Principal.UserName}'
      has moved group '{activeGroup.Name}' over group '{overGroup.Name}'
      in workspace '{workspace.Name}'"
    );

    return Unit.Value;
  }
}

