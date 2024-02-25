using Microsoft.Extensions.Logging;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Groups.CreateGroup;

/// <summary>
/// Command handler responsible for creating a new group within a workspace.
/// </summary>
/// <since>0.0.1</since>
public class CreateGroupCommandHandler(IWorkspaceStore Workspaces, ILogger<CreateGroupCommandHandler> logger) : ICommandHandler<CreateGroupCommand, GroupId>
{
  /// <summary>
  /// Handles the creation of a new group within a workspace.
  /// </summary>
  /// <param name="request">The <see cref="CreateGroupCommand"/> containing the information needed to create the group.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>The unique identifier of the newly created group.</returns>
  public async Task<GroupId> Handle(CreateGroupCommand request, CancellationToken cancellationToken)
  {
    // Retrieve the workspace where the group will be created
    var workspace = await Workspaces.Get(request.WorkspaceId);

    if (workspace is null)
      throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    // Check if the principal is the owner of the workspace
    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(workspace.Id, request.Principal.Id);

    // Create the new group within the workspace
    var group = workspace.CreateGroup(request.Name);

    // Save changes to the workspace
    await Workspaces.SaveChanges();

    // Log useful information about performed action.
    logger.LogInformation(
        @$"Principal '{request.Principal.UserName}'
        has created group '{request.Name}'
        within workspace '{workspace.Name}'"
    );

    return group.Id;
  }
}

