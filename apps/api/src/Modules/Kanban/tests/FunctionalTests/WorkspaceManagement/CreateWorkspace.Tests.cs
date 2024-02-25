using FluentAssertions;
using static FluentAssertions.FluentActions;

using Yak.Core.Common;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.Features.Workspaces.GetWorkspaceById;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.WorkspaceManagement;

public class CreateWorkspaceTests : TestBase
{
  [Fact]
  public async Task UserShouldBeAbleToCreateAndRetrieveWorkspace()
  {
    // -- Act -- //
    var id = await Mediator.Send(
      new CreateWorkspaceCommand(
        WorkspaceName.Validate("Jon's Workspace").OrThrow(),
        Jon
      )
    );

    // -- Verify -- //
    var workspace = await Mediator.Send(
      new GetWorkspaceByIdQuery(id, Jon)
    );

    workspace.Id.Should().Be(id.Value);
    workspace.Name.Should().Be("Jon's Workspace");
  }


  [Fact]
  public async Task ShouldThrowAnExceptionWhenWorkspaceNameIsNotUnique()
  {
    // -- Setup -- //
    var name = WorkspaceName.Validate("Project").OrThrow();

    await Mediator.Send(new CreateWorkspaceCommand(name, Jon));

    // There maybe be two workspaces with the same name but with diferent owner.
    // (workspace name is unique across workspaces of specific user)
    await Mediator.Send(new CreateWorkspaceCommand(name, Daniel));

    // -- Act & Verify -- //
    await Awaiting(() => Mediator.Send(new CreateWorkspaceCommand(name, Jon)))
      .Should()
      .ThrowExactlyAsync<KanbanError.DuplicateWorkspace>(
        "The same owner shall not have two workspaces with the same name"
      );
  }
}