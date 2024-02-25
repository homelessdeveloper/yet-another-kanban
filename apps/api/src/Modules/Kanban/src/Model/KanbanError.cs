using Yak.Core.Common;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents custom errors specific to the Kanban application.
/// </summary>
public static class KanbanError
{
  /// <summary>
  /// Error indicating that a workspace with the same name already exists.
  /// </summary>
  public class DuplicateWorkspace : ApplicationError
  {
    /// <summary>
    /// The name of the duplicate workspace.
    /// </summary>
    public WorkspaceName WorkspaceName { get; }

    public DuplicateWorkspace(WorkspaceName name) : base($"Workspace with name '{name}'", 409)
    {
      WorkspaceName = name;
    }
  }

  /// <summary>
  /// Error indicating that a user does not own a workspace.
  /// </summary>
  public class WorkspaceOwnership : ApplicationError
  {
    /// <summary>
    /// The ID of the workspace.
    /// </summary>
    public WorkspaceId WorkspaceId { get; }

    /// <summary>
    /// The ID of the user.
    /// </summary>
    public UserId UserId { get; }

    public WorkspaceOwnership(WorkspaceId workspaceId, UserId userId) : base(
      $"Workspace with id: '{workspaceId}' does not belong to user: '{userId}'", 409)
    {
      UserId = userId;
      WorkspaceId = workspaceId;
    }
  }

  /// <summary>
  /// Error indicating that a group with the same name already exists in a workspace.
  /// </summary>
  public class DuplicateGroup : ApplicationError
  {
    /// <summary>
    /// The name of the duplicate group.
    /// </summary>
    public GroupName GroupName { get; }

    /// <summary>
    /// The name of the workspace.
    /// </summary>
    public WorkspaceName WorkspaceName { get; }

    public DuplicateGroup(GroupName groupName, WorkspaceName workspaceName) : base(
      $"Group with name '{groupName}' already exists in workspace '{workspaceName}'", 409)
    {
      GroupName = groupName;
      WorkspaceName = workspaceName;
    }
  }

  /// <summary>
  /// Error indicating that a workspace was not found by its ID.
  /// </summary>
  public class WorkspaceNotFoundById : ApplicationError
  {
    /// <summary>
    /// The ID of the workspace.
    /// </summary>
    public WorkspaceId WorkspaceId { get; }

    public WorkspaceNotFoundById(WorkspaceId id) : base($"Could not find workspace with id: {id}", 404)
    {
      WorkspaceId = id;
    }
  }

  /// <summary>
  /// Error indicating that a workspace was not found by its name.
  /// </summary>
  public class WorkspaceNotFoundByName : ApplicationError
  {
    /// <summary>
    /// The name of the workspace.
    /// </summary>
    public WorkspaceName WorkspaceName { get; }

    public WorkspaceNotFoundByName(WorkspaceName name) : base($"Could not find workspace with name: {name}", 404)
    {
      WorkspaceName = name;
    }
  }

  /// <summary>
  /// Error indicating that a group was not found by its ID.
  /// </summary>
  public class GroupNotFoundById : ApplicationError
  {
    /// <summary>
    /// The ID of the group.
    /// </summary>
    public GroupId GroupId { get; }

    public GroupNotFoundById(GroupId id) : base($"Could not find group with id {id}", 404)
    {
      GroupId = id;
    }
  }

  /// <summary>
  /// Error indicating that an assignment was not found by its ID.
  /// </summary>
  public class AssignmentNotFoundById : ApplicationError
  {
    /// <summary>
    /// The ID of the assignment.
    /// </summary>
    public AssignmentId AssignmentId { get; }

    public AssignmentNotFoundById(AssignmentId id) : base($"Could not find assignment with id {id}", 404)
    {
      AssignmentId = id;
    }
  }
}



