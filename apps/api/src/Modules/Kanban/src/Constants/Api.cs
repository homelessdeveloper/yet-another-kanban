namespace Yak.Modules.Kanban.Constants;

/// <summary>
/// A collection of API related constants
/// </summary>
/// <since>0.0.1</since>
public static class Api
{
  /// <summary>
  /// Provides constants for API tags.
  /// </summary>
  public static class Tags
  {
    /// <summary>
    /// Tag for workspace-related endpoints.
    /// </summary>
    /// <since>0.0.1</since>
    public const string Workspaces = "Workspaces";

    /// <summary>
    /// Tag for group-related endpoints.
    /// </summary>
    /// <since>0.0.1</since>
    public const string Groups = "Groups";

    /// <summary>
    /// Tag for assignment-related endpoints.
    /// </summary>
    /// <since>0.0.1</since>
    public const string Assignments = "Assignments";
  }

  /// <summary>
  /// Provides paths for API endpoints.
  /// </summary>
  public static class Paths
  {
    /// <summary>
    /// Base path for API endpoints.
    /// </summary>
    /// <since>0.0.1</since>
    public const string Base = "Api";

    /// <summary>
    /// Path for workspace-related endpoints.
    /// </summary>
    /// <since>0.0.1</since>
    [Obsolete]
    public const string Workspaces = $"{Base}/Workspaces";


    /// <summary>
    /// Provides functionality related to workspaces.
    /// </summary>
    /// <remarks>
    /// This class contains methods to manage workspaces, groups, and assignments.
    /// </remarks>
    /// <since>0.0.1</since>
    public static class Workspace
    {
      // ------------------------------------------------------------ //
      // WORKSPACE RELATED
      // ------------------------------------------------------------ //

      /// <summary>
      /// Creates new workspace
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace to retrieve.</param>
      /// <returns>The workspace with the specified ID.</returns>
      /// <since>0.0.1</since>
      public const string CreateWorkspace = $"{Base}/Workspaces";

      /// <summary>
      /// Retrieves a workspace by its ID.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace to retrieve.</param>
      /// <returns>The workspace with the specified ID.</returns>
      /// <since>0.0.1</since>
      public const string GetWorkspaceById = $"{Base}/Workspaces/{{workspaceId:guid}}";

      /// <summary>
      /// Lists all workspaces.
      /// </summary>
      /// <returns>A list of all workspaces.</returns>
      /// <since>0.0.1</since>
      public const string ListWorksapces = $"{Base}/Workspaces";

      /// <summary>
      /// Renames a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace to rename.</param>
      /// <param name="newName">The new name for the workspace.</param>
      /// <since>0.0.1</since>
      public const string RenameWorksapce = $"{Base}/Workspaces/{{workspaceId:guid}}";

      /// <summary>
      /// Deletes a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace to delete.</param>
      /// <since>0.0.1</since>
      public const string DeleteWorkspace = $"{Base}/Workspaces/{{workspaceId:guid}}";


      // ------------------------------------------------------------ //
      // GROUP RELATED
      // ------------------------------------------------------------ //

      /// <summary>
      /// Creates a new group in a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace where the group will be created.</param>
      /// <param name="groupName">The name of the new group.</param>
      /// <since>0.0.1</since>
      public const string CreateGroup = $"{Base}/Workspaces/{{workspaceId:guid}}/CreateGroup";

      /// <summary>
      /// Renames a group in a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the group.</param>
      /// <param name="groupId">The ID of the group to rename.</param>
      /// <param name="newName">The new name for the group.</param>
      /// <since>0.0.1</since>
      public const string RenameGroup = $"{Base}/Workspaces/{{workspaceId:guid}}/RenameGroup/{{groupId:guid}}";

      /// <summary>
      /// Deletes a group from a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the group.</param>
      /// <param name="groupId">The ID of the group to delete.</param>
      /// <since>0.0.1</since>
      public const string DeleteGroup = $"{Base}/Workspaces/{{workspaceId:guid}}/DeleteGroup/{{groupId:guid}}";

      /// <summary>
      /// Moves a group to a new position in the workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the group.</param>
      /// <param name="groupId">The ID of the group to move.</param>
      /// <param name="newIndex">The new index for the group.</param>
      /// <since>0.0.1</since>
      public const string MoveGroupOver = $"{Base}/Workspaces/{{workspaceId:guid}}/MoveGroupOver/{{activeGroupId:guid}}/{{overGroupId:guid}}";

      // ------------------------------------------------------------ //
      // ASSIGNMENT RELATED
      // ------------------------------------------------------------ //

      /// <summary>
      /// Creates a new assignment in a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace where the assignment will be created.</param>
      /// <param name="assignmentName">The name of the new assignment.</param>
      /// <since>0.0.1</since>
      public const string CreateAssignment = $"{Base}/Workspaces/{{workspaceId:guid}}/CreateAssignment";

      /// <summary>
      /// Updates an existing assignment in a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
      /// <param name="assignmentId">The ID of the assignment to update.</param>
      /// <param name="newName">The new name for the assignment.</param>
      /// <since>0.0.1</since>
      public const string UpdateAssignment = $"{Base}/Workspaces/{{workspaceId:guid}}/UpdateAssignment/{{assignmentId:guid}}";

      /// <summary>
      /// Deletes an assignment from a workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
      /// <param name="assignmentId">The ID of the assignment to delete.</param>
      /// <since>0.0.1</since>
      public const string DeleteAssignment = $"{Base}/Workspaces/{{workspaceId:guid}}/DeleteAssignment/{{assignmentId:guid}}";

      /// <summary>
      /// Moves an assignment to a new position in the workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
      /// <param name="assignmentId">The ID of the assignment to move.</param>
      /// <param name="newIndex">The new index for the assignment.</param>
      /// <since>0.0.1</since>
      public const string MoveAssignmentOver = $"{Base}/Workspaces/{{workspaceId:guid}}/MoveAssignmentOver/{{activeAssignmentId}}/{{overAssignmentId}}";

      /// <summary>
      /// Moves an assignment to a new group within the workspace.
      /// </summary>
      /// <param name="workspaceId">The ID of the workspace containing the assignment.</param>
      /// <param name="assignmentId">The ID of the assignment to move.</param>
      /// <param name="newGroupId">The ID of the new group for the assignment.</param>
      /// <since>0.0.1</since>
      public const string MoveAssignmentTo = $"{Base}/Workspaces/{{workspaceId:guid}}/MoveAssignmentTo/{{assignmentId}}/{{groupId}}";
    }
  }
}


