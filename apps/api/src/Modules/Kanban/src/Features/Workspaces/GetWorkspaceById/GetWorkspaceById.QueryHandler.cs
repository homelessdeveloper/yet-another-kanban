

using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Features.Common;

namespace Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;

/// <summary>
/// Query handler for retrieving a workspace by its ID.
/// </summary>
/// <since>0.0.1</since>
public class GetWorkspaceByIdQueryHandler(IWorkspaceStore workspaces)
  : IQueryHandler<GetWorkspaceByIdQuery, WorkspaceResponse>
{
  /// <summary>
  /// Handles the get workspace by ID query.
  /// </summary>
  /// <param name="request">The get workspace by ID query.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<WorkspaceResponse> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
  {
    var workspace = await workspaces.Get(request.WorkspaceId);

    if (workspace is null) throw new KanbanError.WorkspaceNotFoundById(request.WorkspaceId);

    if (workspace.OwnerId != request.Principal.Id)
      throw new KanbanError.WorkspaceOwnership(request.WorkspaceId, request.Principal.Id);

    return workspace.ToWorkspaceResponse();
  }
}

