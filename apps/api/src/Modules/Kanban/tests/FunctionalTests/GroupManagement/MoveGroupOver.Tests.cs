using FluentAssertions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Groups.CreateGroup;
using Yak.Modules.Kanban.Features.Groups.MoveGroupOver;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.GroupManagement;

public class MoveGroupOver : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToMoveGroupsWithinWorkspace()
  {
    // -- Setup -- //
    var groupAId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Group A").OrThrow(),
        Principal: Jon
      )
    );

    var groupBId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Group B").OrThrow(),
        Principal: Jon
      )
    );

    var groupCId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Group C").OrThrow(),
        Principal: Jon
      )
    );

    var groupDId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Group D").OrThrow(),
        Principal: Jon
      )
    );

    var groupEId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Group E").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    await Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupEId,
        OverGroupId: groupAId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupAId,
        OverGroupId: groupCId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupEId,
        OverGroupId: groupDId,
        Principal: Jon
      )
    );

    await Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupBId,
        OverGroupId: groupCId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));
    workspace.Groups
      .Select(g => g.Name)
      .ToList()
      .Should()
      .ContainInOrder(
        "Group C",
        "Group B",
        "Group A",
        "Group D",
        "Group E"
      );
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceDoesNotExists()
  {
    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: WorkspaceId.Random(),
        ActiveGroupId: GroupId.Random(),
        OverGroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    // -- Verify --//
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenGroupDoesNotExists()
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
    var act1 = () => Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupId,
        OverGroupId: GroupId.Random(),
        Principal: Jon
      )
    );

    var act2 = () => Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: GroupId.Random(),
        OverGroupId: groupId,
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act1.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
    await act2.Should().ThrowExactlyAsync<KanbanError.GroupNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var groupAId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupName.Validate("Group A").OrThrow(),
        Principal: Jon
      )
    );

    var groupBId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        GroupName.Validate("Group B").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new MoveGroupOverCommand(
        WorkspaceId: JonsWorkspaceId,
        ActiveGroupId: groupAId,
        OverGroupId: groupBId,
        Principal: Daniel
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}