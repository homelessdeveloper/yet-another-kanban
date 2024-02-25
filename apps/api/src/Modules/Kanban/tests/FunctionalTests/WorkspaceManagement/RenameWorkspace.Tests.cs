using FluentAssertions;
using static FluentAssertions.FluentActions;
using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Features.Workspaces.RenameWorkspace;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.WorkspaceManagement;

public class RenameWorkspaceTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToRenameWorkspace()
  {
    // -- Setup -- //
    var id = await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("Jon's Workspace").OrThrow(),
        Jon
      )
    );

    // -- Act -- //
    var name = WorkspaceName.Validate("Project").OrThrow();
    await Mediator.Send(new RenameWorkspaceCommand(id, name, Jon));

    // -- Verity -- //
    var workspace = await Mediator.Send(new GetWorkspaceByIdQuery(id, Jon));
    workspace.Name.Should().Be(name);
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceNameIsNotUnique()
  {
    // -- Setup -- //
    var id = await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("School").OrThrow(),
        Jon
      )
    );

    await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("Project").OrThrow(),
        Jon
      )
    );

    // -- Act & Verify -- //
    var name = WorkspaceName.Validate("Project").OrThrow();
    await Awaiting(() => Mediator.Send(new RenameWorkspaceCommand(id, name, Jon)))
      .Should().ThrowExactlyAsync<KanbanError.DuplicateWorkspace>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenWorkspaceDoesNotExists()
  {
    // -- Act & Verify -- //
    var nonExistingWorkspaceId = WorkspaceId.Random();
    var name = WorkspaceName.Validate("Project").OrThrow();

    await Awaiting(() => Mediator.Send(new RenameWorkspaceCommand(nonExistingWorkspaceId, name, Jon)))
      .Should()
      .ThrowExactlyAsync<KanbanError.WorkspaceNotFoundById>();
  }

  [Fact]
  public async Task ShouldThrowAnErrorWhenUserDoesNotOwnWorkspace()
  {
    // -- Setup -- //
    var workspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("Project").OrThrow(),
        Jon
      )
    );

    // -- Act -- //
    var act = () => Mediator.Send(
      new RenameWorkspaceCommand(
        WorkspaceId: workspaceId,
        WorkspaceName: WorkspaceName.Validate("Alexandr's Workspace").OrThrow(),
        Principal: Alexander
      )
    );

    // -- Verify -- //
    await act.Should().ThrowExactlyAsync<KanbanError.WorkspaceOwnership>();
  }
}