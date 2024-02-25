using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.CreateAssignment;

/// <summary>
/// Handles the command to make an assignment.
/// </summary>
/// <since>0.0.1</since>
public class CreateAssignmentCommandHandler(
  IWorkspaceStore workspaces,
  ILogger<CreateAssignmentCommandHandler> logger
)
  : ICommandHandler<CreateAssignmentCommand, AssignmentId>
{
  /// <summary>
  /// Handles the creation of a new assignment.
  /// </summary>
  /// <param name="request">The command to make the assignment.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>The ID of the created assignment.</returns>
  public async Task<AssignmentId> Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
  {
    // Get the workspace in which we are going to create assignment.
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);


    // Check if principal can create an assignment.
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(workspace.Id, request.Principal.Id);

    // Create the assignment
    var assignment = workspace.CreateAssignment(
      request.GroupId,
      request.AssignmentTitle,
      request.Description
    );

    // Persist changes made to the workspace and publish the events.
    await workspaces.SaveChanges();

    // Log useful information.
    logger.LogInformation(
      @$"Principal '{request.Principal.UserName}'
      has created assignment '{assignment.Title}'"
    );

    return assignment.Id;
  }
}

