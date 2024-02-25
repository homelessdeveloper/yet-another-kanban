using Yak.Core.Abstractions;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents domain events specific to the Kanban application.
/// </summary>
public static class KanbanEvent
{
  // ------------------------------------------------------------ //
  // Workspace Related
  // ------------------------------------------------------------ //

  /// <summary>
  /// Event indicating that a new workspace has been created.
  /// </summary>
  public record WorkspaceCreated(WorkspaceId WorkspaceId, WorkspaceName WorkspaceName, UserId OwnerId) : IDomainEvent;

  /// <summary>
  /// Event indicating that a workspace has been renamed.
  /// </summary>
  public record WorkspaceRenamed(WorkspaceId WorkspaceId, WorkspaceName OldName, WorkspaceName NewName) : IDomainEvent;

  /// <summary>
  /// Event indicating that a workspace has been deleted.
  /// </summary>
  public record WorkspaceDeleted(WorkspaceId WorkspaceId) : IDomainEvent;

  // ------------------------------------------------------------ //
  // Group Related
  // ------------------------------------------------------------ //

  /// <summary>
  /// Event indicating that a new group has been created in a workspace.
  /// </summary>
  public record GroupCreated(
    WorkspaceId WorkspaceId,
    GroupId GroupId,
    GroupName GroupName
  ) : IDomainEvent;

  /// <summary>
  /// Event indicating that a group has been renamed in a workspace.
  /// </summary>
  public record GroupRenamed(
    WorkspaceId WorkspaceId,
    GroupId GroupId,
    GroupName OldName,
    GroupName NewName
  ) : IDomainEvent;

  /// <summary>
  /// Event indicating that a group has been deleted from a workspace.
  /// </summary>
  public record GroupDeleted(
    WorkspaceId WorkspaceId,
    GroupId GroupId
  ) : IDomainEvent;

  // ------------------------------------------------------------ //
  // Assignment Related
  // ------------------------------------------------------------ //

  /// <summary>
  /// Event indicating that a new assignment has been created in a group.
  /// </summary>
  public record AssignmentCreated(
    WorkspaceId WorkspaceId,
    GroupId GroupId,
    AssignmentId AssignmentId,
    AssignmentTitle Title,
    string? Description
  ) : IDomainEvent;

  /// <summary>
  /// Event indicating that an assignment has been updated in a group.
  /// </summary>
  public record AssignmentUpdated(
    WorkspaceId WorkspaceId,
    GroupId GroupId,
    AssignmentId AssignmentId,
    AssignmentTitle Title,
    string? Description
  ) : IDomainEvent;

  /// <summary>
  /// Event indicating that an assignment has been deleted from a group.
  /// </summary>
  public record AssignmentDeleted(
    WorkspaceId WorkspaceId,
    GroupId GroupId,
    AssignmentId AssignmentId
  ) : IDomainEvent;
}



