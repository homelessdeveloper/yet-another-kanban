using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Yak.Core.Cqrs;
using Yak.Modules.Kanban.Infrastructure;

namespace Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

/// <summary>
/// Query handler for listing all workspaces that a user owns.
/// </summary>
/// <since>0.0.1</since>
public class ListWorkspacesQueryHandler(KanbanDbContext kanbanDbContext)
  : IQueryHandler<ListWorkspacesQuery, IEnumerable<ListWorkspacesQueryResult>>
{
  /// <summary>
  /// Handles the list workspaces query.
  /// </summary>
  /// <param name="request">The list workspaces query.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task<IEnumerable<ListWorkspacesQueryResult>> Handle(ListWorkspacesQuery request,
    CancellationToken cancellationToken)
  {
    var principal = request.Principal;
    var workspaces = await kanbanDbContext.Workspaces
      .Where(w => w.OwnerId == request.Principal.Id)
      .ToListAsync(cancellationToken);

    return workspaces
      .Where(workspace => workspace.OwnerId == principal.Id)
      .Select(workspace => workspace.ToListWorkspaceQueryResult());
  }
}



