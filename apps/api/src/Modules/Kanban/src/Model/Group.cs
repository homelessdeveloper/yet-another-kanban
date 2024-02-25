
namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents a group within a workspace.
/// </summary>
public class Group
{
  // ------------------------------------------------------------ //
  // PROPERTIES
  // ------------------------------------------------------------ //

  /// <summary>
  /// Gets the ID of the group.
  /// </summary>
  public GroupId Id { get; private set; }

  /// <summary>
  /// Gets or sets the name of the group.
  /// </summary>
  public GroupName Name { get; internal set; }

  /// <summary>
  /// Gets or sets the position of the group.
  /// </summary>
  public uint Position { get; set; }

  /// <summary>
  /// Gets or sets the list of assignments in the group.
  /// </summary>
  public List<Assignment> Assignments { get; internal set; } = new();

  /// <summary>
  /// Gets or sets the ID of the workspace to which the group belongs.
  /// </summary>
  public WorkspaceId WorkspaceId { get; internal set; }

  /// <summary>
  /// Gets or sets the workspace to which the group belongs.
  /// </summary>
  public Workspace? Workspace { get; internal set; }

  // ------------------------------------------------------------ //
  // CONSTRUCTORS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Private constructor used by Entity Framework Core.
  /// </summary>
  private Group()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Group"/> class.
  /// </summary>
  /// <param name="id">The ID of the group.</param>
  /// <param name="name">The name of the group.</param>
  /// <param name="workspaceId">The ID of the workspace to which the group belongs.</param>
  /// <param name="position">The position of the group.</param>
  public Group(
    GroupId id,
    GroupName name,
    WorkspaceId workspaceId,
    uint position
  )
  {
    Id = id;
    Name = name;
    Position = position;
    WorkspaceId = workspaceId;
  }

  // ------------------------------------------------------------ //
  // STATIC CONSTRUCTORS (FACTORIES)
  // ------------------------------------------------------------ //

  /// <summary>
  /// Creates a new group with a random ID.
  /// </summary>
  /// <param name="name">The name of the group.</param>
  /// <param name="workspaceId">The ID of the workspace to which the group belongs.</param>
  /// <param name="position">The position of the group.</param>
  /// <returns>A new group instance.</returns>
  public static Group Make(
    GroupName name,
    WorkspaceId workspaceId,
    uint position = 0
  )
  {
    return new Group(
      GroupId.Random(),
      name,
      workspaceId,
      position
    );
  }

  // ------------------------------------------------------------ //
  // IMPLICIT CONVERSIONS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Implicitly converts a <see cref="Group"/> to a <see cref="GroupId"/>.
  /// </summary>
  /// <param name="group">The group to convert.</param>
  /// <returns>The ID of the group.</returns>
  public static implicit operator GroupId(Group group) => group.Id;
}



