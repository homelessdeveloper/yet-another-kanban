using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents the name of a workspace.
/// </summary>
public class WorkspaceName : NewType<WorkspaceName, string>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="WorkspaceName"/> class with the specified value.
  /// </summary>
  /// <param name="value">The string value of the WorkspaceName.</param>
  public WorkspaceName(string value) : base(value)
  {
  }

  /// <summary>
  /// Validates the specified name to ensure it meets certain criteria.
  /// </summary>
  /// <param name="name">The name to validate.</param>
  /// <returns>A validation result containing either the validated WorkspaceName or a sequence of errors.</returns>
  public static Validation<string, WorkspaceName> Validate(string name)
  {
    var value = name.Trim();
    List<string> errors = new();

    if (string.IsNullOrWhiteSpace(value))
      errors.Add("Workspace name cannot be empty.");

    if (value.Length < 3)
      errors.Add("Workspace name must be at least 3 characters long.");

    if (value.Length > 255)
      errors.Add("Workspace name cannot be longer than 255 characters");

    if (errors.Any()) return errors.ToSeq();

    return new WorkspaceName(value);
  }

  /// <summary>
  /// Implicitly converts a WorkspaceName to a string.
  /// </summary>
  /// <param name="name">The WorkspaceName to convert.</param>
  /// <returns>The string value of the WorkspaceName.</returns>
  public static implicit operator string(WorkspaceName name) => name.Value;
}
