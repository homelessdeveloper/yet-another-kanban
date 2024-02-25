namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents an assignment within a group.
/// </summary>
public class Assignment
{
  // ------------------------------------------------------------ //
  // PROPERTIES
  // ------------------------------------------------------------ //

  /// <summary>
  /// Gets the ID of the assignment.
  /// </summary>
  public AssignmentId Id { get; private set; }

  /// <summary>
  /// Gets or sets the title of the assignment.
  /// </summary>
  public AssignmentTitle Title { get; set; }

  /// <summary>
  /// Gets or sets the description of the assignment.
  /// </summary>
  public string? Description { get; set; }

  /// <summary>
  /// Gets or sets the ID of the group to which the assignment belongs.
  /// </summary>
  public GroupId GroupId { get; set; }

  /// <summary>
  /// Gets or sets the position of the assignment within the group.
  /// </summary>
  public uint Position { get; set; }

  // ------------------------------------------------------------ //
  // CONSTRUCTORS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Private constructor used by Entity Framework Core.
  /// </summary>
  private Assignment()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="Assignment"/> class.
  /// </summary>
  /// <param name="id">The ID of the assignment.</param>
  /// <param name="title">The title of the assignment.</param>
  /// <param name="description">The description of the assignment.</param>
  /// <param name="groupId">The ID of the group to which the assignment belongs.</param>
  /// <param name="position">The position of the assignment within the group.</param>
  public Assignment(
    AssignmentId id,
    AssignmentTitle title,
    string? description,
    GroupId groupId,
    uint position
  )
  {
    Id = id;
    Title = title;
    Description = description;
    GroupId = groupId;
    Position = position;
  }

  // ------------------------------------------------------------ //
  // STATIC CONSTRUCTORS (FACTORIES)
  // ------------------------------------------------------------ //

  /// <summary>
  /// Creates a new assignment with a random ID.
  /// </summary>
  /// <param name="title">The title of the assignment.</param>
  /// <param name="description">The description of the assignment.</param>
  /// <param name="groupId">The ID of the group to which the assignment belongs.</param>
  /// <param name="position">The position of the assignment within the group.</param>
  /// <returns>A new assignment instance.</returns>
  public static Assignment Make(
    AssignmentTitle title,
    string? description,
    GroupId groupId,
    uint position = 0
  )
  {
    return new Assignment(
      AssignmentId.Random(),
      title,
      description,
      groupId,
      position
    );
  }

  // ------------------------------------------------------------ //
  // IMPLICIT CONVERSIONS
  // ------------------------------------------------------------ //

  /// <summary>
  /// Implicitly converts an assignment to its ID.
  /// </summary>
  /// <param name="assignment">The assignment to convert.</param>
  /// <returns>The ID of the assignment.</returns>
  public static implicit operator AssignmentId(Assignment assignment) => assignment.Id;
}

