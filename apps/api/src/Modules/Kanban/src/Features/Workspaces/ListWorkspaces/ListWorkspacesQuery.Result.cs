
namespace Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

/// <summary>
/// Represents the result of listing workspaces, containing the ID and name of each workspace.
/// </summary>
/// <since>0.0.1</since>
public record ListWorkspacesQueryResult(Guid Id, string Name);



