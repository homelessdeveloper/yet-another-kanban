
namespace Yak.Modules.Kanban.Features.Groups.CreateGroup;

/// <summary>
/// Represents a request to create a new group.
/// </summary>
/// <param name="Name">The name of the group to create.</param>
/// <since>0.0.1</since>
public record CreateGroupRequest(
  string Name
);
