using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentTo;

/// <summary>
/// Command handler for moving an assignment to a group within a workspace.
/// </summary>
/// <since>0.0.1</since>
public class MoveAssignmentToCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<MoveAssignmentToCommandHandler> logger
) : ICommandHandler<MoveAssignmentToCommand, Unit>
{
  /// <summary>
  /// Handles the move assignment command.
  /// </summary>
  /// <param name="request">The move assignment command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(MoveAssignmentToCommand request, CancellationToken cancellationToken)
  {
    // Get workspace in which we are going to move assignment
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if principal can move assignments.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Get the assignment and the group.
    // We need this for the logging purposes.
    var assignment = workspace.GetAssignment(request.AssignmentId);
    var group = workspace.GetGroup(request.GroupId);

    // Move the assignment
    workspace.MoveAssignment(request.AssignmentId, request.GroupId, 0);

    // Persist changes and publish the event
    await workspaces.SaveChanges();

    // Log useful information about the action.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
      has moved assignment '{assignment.Title} | ({assignment.Id})'
      to group '{group.Name}'
      within workspace '{workspace.Name}'"
    );

    return Unit.Value;
  }
}


