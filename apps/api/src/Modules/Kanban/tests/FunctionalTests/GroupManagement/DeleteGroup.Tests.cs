using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Groups.CreateGroup;
using Yak.Modules.Kanban.Features.Groups.DeleteGroup;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.GroupManagement;

public class DeleteGroupTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToDeleteGroup()
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
    await Mediator.Send(
      new DeleteGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: groupId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(
      new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon)
    );

    workspace.Groups.Should().NotContain(g => g.Id == groupId.Value);
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new DeleteGroupCommand(
        WorkspaceId: WorkspaceId.Random(),
        GroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenGroupDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new DeleteGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var groupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new DeleteGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupId: groupId,
        Principal: Alexander
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}