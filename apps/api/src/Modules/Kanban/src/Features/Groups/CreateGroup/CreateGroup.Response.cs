
namespace Yak.Modules.Kanban.Features.Groups.CreateGroup;

/// <summary>
/// Represents a response containing the unique identifier of the newly created group.
/// </summary>
/// <param name="Id">The unique identifier of the newly created group.</param>
public record CreateGroupResponse(Guid Id);
