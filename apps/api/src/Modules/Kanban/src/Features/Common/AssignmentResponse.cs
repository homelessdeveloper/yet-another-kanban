using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.Features.Common;

/// <summary>
/// Represents the response for an assignment.
/// </summary>
/// <param name="Id">The ID of the assignment.</param>
/// <param name="Title">The title of the assignment.</param>
/// <param name="Description">The description of the assignment (optional). </param>
/// <param name="Position">The position of the assignment.</param>
/// <since>0.0.1</since>
public record AssignmentResponse(
  Guid Id,
  string Title,
  string? Description,
  uint Position
);

/// <summary>
/// Extension methods for the Assignment class.
/// </summary>
public static class AssignmentExtensions
{
  /// <summary>
  /// Converts an Assignment object to an AssignmentResponse object.
  /// </summary>
  /// <param name="assignment">The Assignment object.</param>
  /// <returns>An AssignmentResponse object.</returns>
  public static AssignmentResponse ToAssignmentResponse(this Assignment assignment)
  {
    return new AssignmentResponse(
      assignment.Id,
      assignment.Title.Value,
      assignment.Description,
      assignment.Position
    );
  }
}
