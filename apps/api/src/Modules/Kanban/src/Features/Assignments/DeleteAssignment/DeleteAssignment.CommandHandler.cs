using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.DeleteAssignment;

/// <summary>
/// Handles the command to delete an assignment.
/// </summary>
/// <since>0.0.1</since>
public class DeleteAssignmentCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<DeleteAssignmentCommandHandler> logger
) : ICommandHandler<DeleteAssignmentCommand, Unit>
{
  /// <summary>
  /// Handles the deletion of an assignment.
  /// </summary>
  /// <param name="request">The command to delete the assignment.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(DeleteAssignmentCommand request, CancellationToken cancellationToken)
  {
    // Get the workspace in which requested assignment located.
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if principal can delete the assignment.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Get the assignment that we are going to delete.
    // (We need it for the logging purposes)
    var assignment = workspace.GetAssignment(request.AssignmentId);

    // Delete the assignment from the workspace
    workspace.DeleteAssignment(request.AssignmentId);

    // Persist changes made to the workspace
    // and publish the events.
    await workspaces.SaveChanges();

    // Log userful information about performed action
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
        has deleted the assignment '{assignment.Title}'
        in workspace '{workspace.Name}'"
    );

    return Unit.Value;
  }
}

