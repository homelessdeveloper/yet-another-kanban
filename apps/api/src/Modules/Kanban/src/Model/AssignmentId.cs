using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents the unique identifier of an assignment.
/// </summary>
public class AssignmentId : NewType<AssignmentId, Guid>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AssignmentId"/> class with the specified value.
  /// </summary>
  /// <param name="value">The value of the assignment ID.</param>
  public AssignmentId(Guid value) : base(value)
  {
  }

  /// <summary>
  /// Implicitly converts an <see cref="AssignmentId"/> to a <see cref="Guid"/>.
  /// </summary>
  /// <param name="id">The assignment ID to convert.</param>
  /// <returns>The value of the assignment ID as a <see cref="Guid"/>.</returns>
  public static implicit operator Guid(AssignmentId id) => id.Value;

  /// <summary>
  /// Generates a new random <see cref="AssignmentId"/>.
  /// </summary>
  /// <returns>A new random <see cref="AssignmentId"/>.</returns>
  public static AssignmentId Random() => AssignmentId.New(Guid.NewGuid());
}

