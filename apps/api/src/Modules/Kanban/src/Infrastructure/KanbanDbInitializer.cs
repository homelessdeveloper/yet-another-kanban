
using Microsoft.EntityFrameworkCore;

namespace Yak.Modules.Kanban.Infrastructure;

/// <summary>
/// Initializes the Kanban database.
/// </summary>
public class KanbanDbInitializer(KanbanDbContext kanbanDbContext)
{
  /// <summary>
  /// Initializes the database asynchronously.
  /// </summary>
  /// <param name="cancellationToken">The cancellation token.</param>
  public async Task InitializeDatabaseAsync(CancellationToken cancellationToken = default)
  {
    await kanbanDbContext.Database.MigrateAsync(cancellationToken);
  }
}
