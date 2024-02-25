using FluentAssertions;

using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Groups.CreateGroup;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.GroupManagement;

public class CreateGroupTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToCreateAndRetrieveGroup()
  {
    var groupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(JonsWorkspaceId, Jon));
    workspace.Groups.Should().ContainSingle(g => g.Id == groupId.Value);
  }


  [Fact]
  public async Task ShouldThrowAnErrorInCaseWorkspaceDoesNotExist()
  {
    var nonExistingWorkspaceId = WorkspaceId.Random();

    var act = () => Mediator.Send(new CreateGroupCommand(
      WorkspaceId: nonExistingWorkspaceId,
      Name: GroupName.Validate("ToDo's").OrThrow(),
      Principal: Jon
    ));

    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseGroupNameIsNotUnique()
  {
    // -- Setup -- //
    await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.DuplicateGroup>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorInCaseUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Alexander
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}