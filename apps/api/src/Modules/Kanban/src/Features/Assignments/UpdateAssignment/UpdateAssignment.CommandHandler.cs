using MediatR;
using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.UpdateAssignment;

/// <summary>
/// Command handler for updating assignment details.
/// </summary>
public class UpdateAssignmentCommandHandler(IWorkspaceStore workspaces, ILogger<UpdateAssignmentCommandHandler> logger)
  : ICommandHandler<UpdateAssignmentCommand, Unit>
{
  /// <summary>
  /// Handles the update assignment command.
  /// </summary>
  /// <param name="request">The update assignment command.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(UpdateAssignmentCommand request, CancellationToken cancellationToken)
  {
    // Get the workspace which contains the assignment.
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if user can update assignments
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Get the assignment.
    // We need it for logging purposes.
    var assignment = workspace.GetAssignment(request.AssignmentId);

    // Update the assignment
    workspace.UpdateAssignment(
      id: request.AssignmentId,
      title: request.Title,
      description: request.Description
    );

    // Persist changes made to the workspace and publish the events
    await workspaces.SaveChanges();

    // Log useful information about the action
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
      has updated assignment '{assignment.Title} | ({assignment.Id})'
      in workspace '{workspace.Name}'
      "
    );

    return Unit.Value;
  }
}


