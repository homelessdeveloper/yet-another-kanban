
using Yak.Core.Cqrs;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Features.Common;


namespace Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;

/// <summary>
/// Represents a query to retrieve a workspace by its ID.
/// </summary>
/// <param name="WorkspaceId">THe ID of the workspace to retrieve.</param>
/// <param name="Principal">The proncipal who is retrieving the workspace.</param>
/// <since>0.0.1</since>
public record GetWorkspaceByIdQuery(
  WorkspaceId WorkspaceId,
  Principal Principal
) : IQuery<WorkspaceResponse>;

