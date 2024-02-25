using Yak.Core.Abstractions;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Model;

public class Workspace : AggregateRoot
{
  // ------------------------------------------------------------ //
  // PROPERTIES
  // ------------------------------------------------------------ //

  /// <summary>
  /// Gets the id of the workspace.
  /// </summary>
  public WorkspaceId Id { get; set; }

  /// <summary>
  /// Gets the name of the workspace.
  /// </summary>
  public WorkspaceName Name { get; set; }

  /// <summary>
  /// Gets the name of the workspace.
  /// </summary>
  public virtual List<Group> Groups { get; set; } = new();

  /// <summary>
  /// Gets the ID of the workspace owner.
  /// </summary>
  public UserId OwnerId { get; private set; }

  // ------------------------------------------------------------ //
  // CONSTRUCTORS
  // ------------------------------------------------------------ //

  /// <summary>
  /// This constructor is only required for the entity framework.
  /// </summary>
  private Workspace()
  {
  }

  /// <summary>
  /// Basic constructor for Workspace
  /// </summary>
  private Workspace(WorkspaceId id, WorkspaceName name, UserId ownerId)
  {
    Id = id;
    Name = name;
    OwnerId = ownerId;
  }

  // ------------------------------------------------------------ //
  // STATIC CONSTRUCTORS (FACTORIES)
  // ------------------------------------------------------------ //

  /// <summary>
  /// Creates new Workspace with random WorkspaceId (Guid)
  /// </summary>
  public static Workspace Create(WorkspaceName name, UserId ownerId)
  {
    var id = WorkspaceId.Random();
    var workspace = new Workspace(id, name, ownerId);

    workspace.PublishEvent(new KanbanEvent.WorkspaceCreated(id, name, ownerId));

    return workspace;
  }
  // ------------------------------------------------------------ //
  // METHODS: Workspace
  // ------------------------------------------------------------ //

  /// <summary>
  /// Renames the workspace.
  /// </summary>
  /// <param name="name">The new name of the workspace.</param>
  /// <remarks>Publishes <see cref="KanbanEvent.WorkspaceRenamed"/></remarks>
  public void Rename(WorkspaceName name)
  {
    var oldName = Name;
    Name = name;

    PublishEvent(
      new KanbanEvent.WorkspaceRenamed(
        Id,
        oldName,
        name
      )
    );
  }


  // ------------------------------------------------------------ //
  // METHODS: GROUP MANAGEMENT
  // ------------------------------------------------------------ //

  /// <summary>
  /// Reorders the groups in the workspace based on their current positions.
  /// </summary>
  private void ReOrderGroups()
  {
    Groups = Groups.Select((group, index) =>
    {
      group.Position = (uint)index;
      return group;
    }).ToList();
  }

  /// <summary>
  /// Gets the group with the specified ID.
  /// </summary>
  /// <param name="id">The ID of the group to retrieve.</param>
  /// <returns>The group with the specified ID.</returns>
  /// <exception cref="KanbanError.GroupNotFoundById">Thrown when no group with the specified ID is found.</exception>
  public Group GetGroup(GroupId id)
  {
    var group = Groups.Find(group => group.Id == id);

    if (group is null)
      throw new KanbanError.GroupNotFoundById(id);

    return group;
  }

  /// <summary>
  /// Creates a new group in the workspace with the specified name.
  /// </summary>
  /// <param name="name">The name of the new group.</param>
  /// <returns>The newly created group.</returns>
  /// <exception cref="KanbanError.DuplicateGroup">Thrown when a group with the same name already exists in the workspace.</exception>
  public Group CreateGroup(GroupName name)
  {
    if (Groups.Any(g => g.Name.Value.ToLower() == name.Value.ToLower()))
    {
      throw new KanbanError.DuplicateGroup(
        name,
        Name
      );
    }

    var group = Group.Make(name, Id, (uint)Groups.Count);

    Groups.Add(group);

    PublishEvent(
      new KanbanEvent.GroupCreated(
        WorkspaceId: Id,
        GroupId: group.Id,
        GroupName: group.Name
      )
    );

    return group;
  }

  /// <summary>
  /// Renames the group with the specified ID to the specified name.
  /// </summary>
  /// <param name="id">The ID of the group to rename.</param>
  /// <param name="name">The new name of the group.</param>
  /// <exception cref="KanbanError.GroupNotFoundById">Thrown when no group with the specified ID is found.</exception>
  /// <exception cref="KanbanError.DuplicateGroup">Thrown when a group with the same name already exists in the workspace.</exception>
  public void RenameGroup(GroupId id, GroupName name)
  {
    var group = GetGroup(id);

    if (Groups.Any(g => g.Name.Value.ToLower() == name.Value.ToLower()))
    {
      throw new KanbanError.DuplicateGroup(
        name,
        Name
      );
    }

    var oldName = group.Name;
    group.Name = name;

    PublishEvent(
      new KanbanEvent.GroupRenamed(
        WorkspaceId: Id,
        GroupId: group.Id,
        OldName: oldName,
        NewName: name
      )
    );
  }

  /// <summary>
  /// Deletes the group with the specified ID from the workspace.
  /// </summary>
  /// <param name="id">The ID of the group to delete.</param>
  /// <returns>True if the group was successfully deleted; otherwise, false.</returns>
  public bool DeleteGroup(GroupId id)
  {
    var group = GetGroup(id);

    Groups.Remove(group);

    PublishEvent(
      new KanbanEvent.GroupDeleted(
        WorkspaceId: Id,
        GroupId: id
      )
    );

    return true;
  }

  /// <summary>
  /// Moves the group with the specified ID to the specified position.
  /// </summary>
  /// <param name="id">The ID of the group to move.</param>
  /// <param name="position">The position to move the group to.</param>
  public void MoveGroup(GroupId id, uint position)
  {
    uint count = (uint)Groups.Count;
    uint newPosition = position >= count ? count : position;

    var group = GetGroup(id);

    Groups.Remove(group);
    Groups.Insert((int)newPosition, group);

    ReOrderGroups();
  }

  /// <summary>
  /// Moves the active group over the specified group.
  /// </summary>
  /// <param name="activeGroupId">The ID of the active group.</param>
  /// <param name="overGroupId">The ID of the group to move the active group over.</param>
  public void MoveGroupOver(GroupId activeGroupId, GroupId overGroupId)
  {
    var activeGroup = GetGroup(activeGroupId);
    var overGroup = GetGroup(overGroupId);
    MoveGroup(activeGroup.Id, overGroup.Position);
  }


  // ------------------------------------------------------------ //
  // METHODS: ASSIGNMENT MANAGEMENT
  // ------------------------------------------------------------ //

  /// <summary>
  /// Reorders the assignments in the group based on their current positions.
  /// </summary>
  /// <param name="groupId">The ID of the group whose assignments are to be reordered.</param>
  private void ReOrderAssignments(GroupId groupId)
  {
    var group = GetGroup(groupId);
    group.Assignments = group.Assignments.Select((assignment, position) =>
    {
      assignment.Position = (uint)position;
      return assignment;
    }).ToList();
  }

  /// <summary>
  /// Gets the assignment with the specified ID.
  /// </summary>
  /// <param name="id">The ID of the assignment to retrieve.</param>
  /// <returns>The assignment with the specified ID.</returns>
  /// <exception cref="KanbanError.AssignmentNotFoundById">Thrown when no assignment with the specified ID is found.</exception>
  public Assignment GetAssignment(AssignmentId id)
  {
    var assignment = Assignments.FirstOrDefault(a => a.Id == id);
    if (assignment is null) throw new KanbanError.AssignmentNotFoundById(id);

    return assignment;
  }

  /// <summary>
  /// Gets all assignments in the workspace.
  /// </summary>
  public IEnumerable<Assignment> Assignments => Groups.SelectMany(g => g.Assignments);

  /// <summary>
  /// Creates a new assignment in the specified group with the specified title and description.
  /// </summary>
  /// <param name="groupId">The ID of the group in which to create the assignment.</param>
  /// <param name="title">The title of the new assignment.</param>
  /// <param name="description">The description of the new assignment.</param>
  /// <returns>The newly created assignment.</returns>
  public Assignment CreateAssignment(
    GroupId groupId,
    AssignmentTitle title,
    string? description = null
  )
  {
    var group = GetGroup(groupId);
    var assignment = Assignment.Make(
      title: title,
      description: description,
      groupId: groupId,
      (uint)group.Assignments.Count
    );

    group.Assignments.Add(assignment);

    PublishEvent(
      new KanbanEvent.AssignmentCreated(
        WorkspaceId: this.Id,
        GroupId: group.Id,
        AssignmentId: assignment.Id,
        Title: assignment.Title,
        Description: assignment.Description
      )
    );

    return assignment;
  }

  /// <summary>
  /// Deletes the assignment with the specified ID from the workspace.
  /// </summary>
  /// <param name="id">The ID of the assignment to delete.</param>
  public void DeleteAssignment(AssignmentId id)
  {
    var assignment = GetAssignment(id);
    var group = GetGroup(assignment.GroupId);
    group.Assignments.Remove(assignment);

    PublishEvent(
      new KanbanEvent.AssignmentDeleted(
        WorkspaceId: this.Id,
        GroupId: group.Id,
        AssignmentId: assignment.Id
      )
    );
  }

  /// <summary>
  /// Updates the assignment with the specified ID with the new title and description.
  /// </summary>
  /// <param name="id">The ID of the assignment to update.</param>
  /// <param name="title">The new title of the assignment.</param>
  /// <param name="description">The new description of the assignment.</param>
  public void UpdateAssignment(
    AssignmentId id,
    AssignmentTitle? title = null,
    string? description = null
  )
  {
    var assignment = GetAssignment(id);
    var hasChanges = false;

    if (title is not null && assignment.Title != title)
    {
      hasChanges = true;
      assignment.Title = title;
    }

    if (description is not null && assignment.Description != description)
    {
      hasChanges = true;
      assignment.Description = description;
    }

    if (!hasChanges) return;

    PublishEvent(
      new KanbanEvent.AssignmentUpdated(
        WorkspaceId: this.Id,
        GroupId: assignment.GroupId,
        AssignmentId: assignment.Id,
        Title: title ?? assignment.Title,
        Description: description ?? assignment.Description
      )
    );
  }

  /// <summary>
  /// Moves the assignment with the specified ID to the specified position in its group.
  /// </summary>
  /// <param name="id">The ID of the assignment to move.</param>
  /// <param name="position">The position to move the assignment to.</param>
  public void MoveAssignment(AssignmentId id, uint position)
  {
    var assignment = GetAssignment(id);
    var group = GetGroup(assignment.GroupId);

    uint count = (uint)group.Assignments.Count;
    uint newPosition = (position >= count ? count : position);

    group.Assignments.Remove(assignment);
    group.Assignments.Insert((int)newPosition, assignment);

    ReOrderAssignments(group.Id);
  }

  /// <summary>
  /// Moves the assignment with the specified ID to the specified position in the target group.
  /// </summary>
  /// <param name="id">The ID of the assignment to move.</param>
  /// <param name="targetGroupId">The ID of the target group.</param>
  /// <param name="position">The position to move the assignment to.</param>
  public void MoveAssignment(AssignmentId id, GroupId targetGroupId, uint position)
  {
    var assignment = GetAssignment(id);
    var originalGroup = GetGroup(assignment.GroupId);
    var targetGroup = GetGroup(targetGroupId);

    uint count = (uint)targetGroup.Assignments.Count;
    uint newPosition = (position >= count ? count : position);

    originalGroup.Assignments.Remove(assignment);
    targetGroup.Assignments.Insert((int)newPosition, assignment);

    ReOrderAssignments(originalGroup);
    ReOrderAssignments(targetGroup);
  }

  /// <summary>
  /// Moves the active assignment over the specified assignment.
  /// </summary>
  /// <param name="activeAssignmentId">The ID of the active assignment.</param>
  /// <param name="overAssignmentId">The ID of the assignment to move the active assignment over.</param>
  public void MoveAssignmentOver(AssignmentId activeAssignmentId, AssignmentId overAssignmentId)
  {
    var activeAssignment = GetAssignment(activeAssignmentId);
    var overAssignment = GetAssignment(overAssignmentId);

    MoveAssignment(
      activeAssignment,
      overAssignment.GroupId,
      overAssignment.Position
    );
  }


  // ------------------------------------------------------------ //
  // IMPLICIT CONVERSIONS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Implicitly converts a Workspace object to its Id.
  /// </summary>
  /// <param name="workspace">The Workspace object to convert.</param>
  /// <returns>The Id of the Workspace.</returns>
  public static implicit operator WorkspaceId(Workspace workspace) => workspace.Id;
}
