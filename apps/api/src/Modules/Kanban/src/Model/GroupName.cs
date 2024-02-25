
using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents the name of a group.
/// </summary>
public class GroupName : NewType<GroupName, string>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="GroupName"/> class with the specified value.
  /// </summary>
  /// <param name="value">The value of the group name.</param>
  internal GroupName(string value) : base(value)
  {
  }

  /// <summary>
  /// Validates the specified name for a group.
  /// </summary>
  /// <param name="name">The name to validate.</param>
  /// <returns>A validated <see cref="GroupName"/> or a sequence of validation errors.</returns>
  public static Validation<string, GroupName> Validate(string name)
  {
    var value = name.Trim();

    List<string> errors = new();

    if (string.IsNullOrWhiteSpace(value))
      errors.Add("Group name cannot be empty.");

    if (value.Length > 255)
      errors.Add("Group name cannot be longer than 255 characters");

    if (errors.Any()) return errors.ToSeq();

    return new GroupName(value);
  }

  /// <summary>
  /// Implicitly converts a <see cref="GroupName"/> to a <see cref="string"/>.
  /// </summary>
  /// <param name="name">The group name to convert.</param>
  /// <returns>The value of the group name as a <see cref="string"/>.</returns>
  public static implicit operator string(GroupName name) => name.Value;
}

