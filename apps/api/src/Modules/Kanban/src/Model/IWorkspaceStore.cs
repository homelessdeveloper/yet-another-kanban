
namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Interface for accessing workspace data from a data store.
/// </summary>
public interface IWorkspaceStore
{
  /// <summary>
  /// Retrieves a workspace by its unique identifier.
  /// </summary>
  /// <param name="id">The unique identifier of the workspace.</param>
  /// <returns>A task representing the asynchronous operation. The task result contains the retrieved workspace, or null if not found.</returns>
  Task<Workspace?> Get(WorkspaceId id);

  /// <summary>
  /// Retrieves a workspace by its name.
  /// </summary>
  /// <param name="name">The name of the workspace.</param>
  /// <returns>A task representing the asynchronous operation. The task result contains the retrieved workspace, or null if not found.</returns>
  Task<Workspace?> Get(WorkspaceName name);

  /// <summary>
  /// Adds a new workspace to the data store.
  /// </summary>
  /// <param name="workspace">The workspace to add.</param>
  void Add(Workspace workspace);

  /// <summary>
  /// Saves any pending changes to the data store.
  /// </summary>
  /// <returns>A task representing the asynchronous operation.</returns>
  Task SaveChanges();

  /// <summary>
  /// Removes a workspace from the data store.
  /// </summary>
  /// <param name="workspace">The workspace to remove.</param>
  void Remove(Workspace workspace);
}

