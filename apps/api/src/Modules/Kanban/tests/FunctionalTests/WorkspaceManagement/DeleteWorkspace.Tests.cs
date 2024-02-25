using FluentAssertions;
using static FluentAssertions.FluentActions;

using Yak.Core.Common;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Features.Workspaces.DeleteWorkspace;

namespace Yak.Modules.Kanban.FunctionalTests.WorkspaceManagement;

public class DeleteWorkspaceTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToDeleteWorkspace()
  {
    // -- Setup -- //
    var workspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("project").OrThrow(),
        Jon
      )
    );


    // -- Act -- //
    await Mediator.Send(new DeleteWorkspaceCommand(workspaceId, Jon));

    // -- Verify -- //
    await Awaiting(() => Mediator.Send(new GetWorkspaceByIdQuery(workspaceId, Jon)))
      .Should()
      .ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceDoesNotExists()
  {
    // -- Act & Verify -- //
    var nonExistingWorkspaceId = WorkspaceId.Random();

    await Awaiting(() => Mediator.Send(new DeleteWorkspaceCommand(nonExistingWorkspaceId, Jon)))
      .Should()
      .ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var workspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Project").OrThrow(),
        Jon
      )
    );

    // -- Act & Verify -- //
    await Awaiting(() => Mediator.Send(new DeleteWorkspaceCommand(workspaceId, Daniel)))
      .Should()
      .ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}
