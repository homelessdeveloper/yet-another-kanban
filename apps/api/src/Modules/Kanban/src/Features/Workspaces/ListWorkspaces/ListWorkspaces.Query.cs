using Yak.Core.Cqrs;

using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

/// <summary>
/// Represents a query to list all workspaces that a user owns.
/// </summary>
/// <param name="Principal"> The principal who is requesting the list of workspaces </param>
/// <since>0.0.1</since>
public record ListWorkspacesQuery(Principal Principal) : IQuery<IEnumerable<ListWorkspacesQueryResult>>;


