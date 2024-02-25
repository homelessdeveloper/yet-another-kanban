using Microsoft.EntityFrameworkCore;
using Yak.Core.Abstractions;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Infrastructure;

public class WorkspaceStore(
  KanbanDbContext ctx,
  IDomainEventPublisher publisher
) : AggregateRepository<Workspace>, IWorkspaceStore
{
  public async Task<Workspace?> Get(WorkspaceId id)
  {
    var workspace = await ctx.Workspaces
      .Include(w => w.Groups.OrderBy(g => g.Position))
      .ThenInclude(g => g.Assignments.OrderBy(a => a.Position))
      .FirstOrDefaultAsync(w => w.Id == id);

    return workspace;
  }


  public async Task<Workspace?> Get(WorkspaceName name)
  {
    var workspace = await ctx.Workspaces
      .Include(w => w.Groups.OrderBy(g => g.Position))
      .ThenInclude(g => g.Assignments.OrderBy(a => a.Position))
      .FirstOrDefaultAsync(w => w.Name == name);

    return workspace;
  }


  public void Add(Workspace workspace)
  {
    ctx.Workspaces.Add(workspace);
  }


  public void Remove(Workspace workspace)
  {
    ctx.Workspaces.Remove(workspace);
  }


  public async Task SaveChanges()
  {
    // Gather all workspaces that has uncommited changes.
    var workspaces = ctx.ChangeTracker
      .Entries<Workspace>()
      .Select(w => w.Entity)
      .ToList();

    // Commit changes that were made before.
    await ctx.SaveChangesAsync();

    // Loop through workspaces.
    foreach (var workspace in workspaces)
    {
      // Loop through each event in each workspace and publish it.
      foreach (var @event in workspace.GetDomainEvents())
      {
        await publisher.Publish(@event);
      }

      // We have to clear inner tracker of those events
      ClearEvents(workspace);
    }
  }
}
