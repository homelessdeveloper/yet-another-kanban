using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Common;

/// <summary>
/// Represents a response containing workspace information.
/// </summary>
/// <param name="Id">The unique identifier of the workspace</param>
/// <param name="Name">The name of the workspace.</param>
/// <param name="Groups">The grups associated with the workspace.</param>
/// <since>0.0.1</since>
public record WorkspaceResponse(
  Guid Id,
  string Name,
  IEnumerable<GroupResponse> Groups
);

/// <summary>
/// Provides extension methods for <see cref="Workspace"/> class.
/// </summary>
public static class WorkspaceExtensions
{
  /// <summary>
  /// Converts a <see cref="Workspace"/> object to a <see cref="WorkspaceResponse"/> object.
  /// </summary>
  /// <param name="workspace">The <see cref="Workspace"/> object to convert.</param>
  /// <returns>A <see cref="WorkspaceResponse"/> object representing the workspace.</returns>
  public static WorkspaceResponse ToWorkspaceResponse(this Workspace workspace)
  {
    return new WorkspaceResponse(
      workspace.Id,
      workspace.Name.Value,
      workspace.Groups
        .Map(group => group.ToGroupResponse())
    );
  }
}
