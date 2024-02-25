using LanguageExt;

namespace Yak.Modules.Kanban.Model;

/// <summary>
/// Represents a unique identifier for a workspace.
/// </summary>
public class WorkspaceId : NewType<WorkspaceId, Guid>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="WorkspaceId"/> class with the specified value.
  /// </summary>
  /// <param name="value">The Guid value of the WorkspaceId.</param>
  public WorkspaceId(Guid value) : base(value)
  {
  }

  /// <summary>
  /// Implicitly converts a WorkspaceId to a Guid.
  /// </summary>
  /// <param name="id">The WorkspaceId to convert.</param>
  /// <returns>The Guid value of the WorkspaceId.</returns>
  public static implicit operator Guid(WorkspaceId id) => id.Value;

  /// <summary>
  /// Generates a new random WorkspaceId (Guid).
  /// </summary>
  /// <returns>A new WorkspaceId with a random Guid value.</returns>
  public static WorkspaceId Random() => New(Guid.NewGuid());
}
