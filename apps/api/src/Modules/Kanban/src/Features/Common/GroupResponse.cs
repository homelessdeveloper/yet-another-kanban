using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Common;

/// <summary>
/// Represents the response for a group.
/// </summary>
/// <param name="Id">The ID of the group</param>
/// <param name="Name">The name of the group</param>
/// <param name="WorkspaceId">The id of the workspace to witch the </param>
/// <param name="Assignments">The assignment within the group</param>
/// <since>0.0.1</since>
public record GroupResponse(
  Guid Id,
  string Name,
  Guid WorkspaceId,
  uint Position,
  IEnumerable<AssignmentResponse> Assignments
);

/// <summary>
/// Extension methods for the Group class.
/// </summary>
public static class GroupExtensions
{
  /// <summary>
  /// Converts a Group object to a GroupResponse object.
  /// </summary>
  /// <param name="group">The Group object.</param>
  /// <returns>A GroupResponse object.</returns>
  public static GroupResponse ToGroupResponse(this Group group)
  {
    return new GroupResponse(
      group.Id,
      group.Name,
      group.WorkspaceId,
      group.Position,
      group.Assignments
        .OrderBy(assignment => assignment.Position)
        .Select(assignment => assignment.ToAssignmentResponse())
    );
  }
}

