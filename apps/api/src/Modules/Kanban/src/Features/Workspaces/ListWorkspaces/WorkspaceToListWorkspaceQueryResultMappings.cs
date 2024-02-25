
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

/// <summary>
/// Contains extension methods to map workspace entities to list workspace query result objects.
/// </summary>
/// <since>0.0.1</since>
public static class WorkspaceToListWorkspaceQueryResultMappings
{
  /// <summary>
  /// Maps a workspace entity to a list workspace query result object.
  /// </summary>
  /// <param name="this">The workspace entity to map.</param>
  /// <returns>A list workspace query result object.</returns>
  public static ListWorkspacesQueryResult ToListWorkspaceQueryResult(this Workspace @this)
  {
    return new ListWorkspacesQueryResult(@this.Id, @this.Name);
  }
}

