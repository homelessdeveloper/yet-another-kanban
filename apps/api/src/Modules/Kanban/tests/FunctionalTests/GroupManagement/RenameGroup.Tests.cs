using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Groups.CreateGroup;
using Yak.Modules.Kanban.Features.Groups.RenameGroup;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.GroupManagement;

public class RenameGroupTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToRenameGroup()
  {
    // -- Setup -- //
    var groupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var newName = GroupName.Validate("Archive").OrThrow();

    await Mediator.Send(
      new RenameGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: groupId,
        Name: newName,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(
      new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon)
    );

    var group = workspace.Groups.FirstOrDefault(g => g.Id == groupId.Value);
    group.Name.Should().Be(newName.Value);
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var groupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var newName = GroupName.Validate("Archive").OrThrow();

    var act = () => Mediator.Send(
      new RenameGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: groupId,
        Name: newName,
        Principal: Alexander
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }


  [Fact]
  public async Task ShouldThrowAnErrorWhenGroupDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new RenameGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: GroupId.Random(),
        Name: GroupName.Validate("Archive").OrThrow(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
  }
}