
using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents the unique identifier of a group.
/// </summary>
public class GroupId : NewType<GroupId, Guid>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="GroupId"/> class with the specified value.
  /// </summary>
  /// <param name="value">The value of the group ID.</param>
  public GroupId(Guid value) : base(value)
  {
  }

  /// <summary>
  /// Implicitly converts a <see cref="GroupId"/> to a <see cref="Guid"/>.
  /// </summary>
  /// <param name="id">The group ID to convert.</param>
  /// <returns>The value of the group ID as a <see cref="Guid"/>.</returns>
  public static implicit operator Guid(GroupId id) => id.Value;

  /// <summary>
  /// Generates a new random <see cref="GroupId"/>.
  /// </summary>
  /// <returns>A new random <see cref="GroupId"/>.</returns>
  public static GroupId Random() => GroupId.New(Guid.NewGuid());
}

