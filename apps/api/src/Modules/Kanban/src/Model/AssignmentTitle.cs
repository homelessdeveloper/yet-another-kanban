using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents the title of an assignment.
/// </summary>
public class AssignmentTitle : NewType<AssignmentTitle, string>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="AssignmentTitle"/> class with the specified value.
  /// </summary>
  /// <param name="value">The value of the assignment title.</param>
  internal AssignmentTitle(string value) : base(value)
  {
  }

  /// <summary>
  /// Validates the specified title for an assignment.
  /// </summary>
  /// <param name="title">The title to validate.</param>
  /// <returns>A validated <see cref="AssignmentTitle"/> or a sequence of validation errors.</returns>
  public static Validation<string, AssignmentTitle> Validate(string title)
  {
    var value = title.Trim();

    List<string> errors = new();

    if (string.IsNullOrWhiteSpace(value))
      errors.Add("Assignment title cannot be empty.");

    if (value.Length > 255)
      errors.Add("Assignment title cannot be longer than 255 characters");

    if (errors.Any()) return errors.ToSeq();

    return new AssignmentTitle(value);
  }

  /// <summary>
  /// Implicitly converts an <see cref="AssignmentTitle"/> to a <see cref="string"/>.
  /// </summary>
  /// <param name="name">The assignment title to convert.</param>
  /// <returns>The value of the assignment title as a <see cref="string"/>.</returns>
  public static implicit operator string(AssignmentTitle name) => name.Value;
}

