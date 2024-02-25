using MediatR;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Assignments.MoveAssignmentOver;

/// <summary>
/// Handles the command to move an assignment over another assignment.
/// </summary>
/// <since>0.0.1</since>
public class MoveAssignmentOver(IWorkspaceStore workspaces) : ICommandHandler<MoveAssignmentOverCommand, Unit>
{
  /// <summary>
  /// Handles the operation to move an assignment over another assignment.
  /// </summary>
  /// <param name="request">The command to move the assignment.</param>
  /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<Unit> Handle(MoveAssignmentOverCommand request, CancellationToken cancellationToken)
  {
    // Get the workspace in which we are going to move assignments
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if the principal can move assignments
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    // Move assignments
    workspace.MoveAssignmentOver(request.ActiveAssignmentId, request.OverAssignmentId);

    // Persist changes made to the workspace and publish the events
    await workspaces.SaveChanges();

    return Unit.Value;
  }
}


