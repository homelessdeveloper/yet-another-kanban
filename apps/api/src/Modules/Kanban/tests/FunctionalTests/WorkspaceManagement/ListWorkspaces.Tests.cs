using FluentAssertions;

using Yak.Core.Common;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.Features.Workspaces.ListWorkspaces;

namespace Yak.Modules.Kanban.FunctionalTests.WorkspaceManagement;

public class ListWorkspacesTests : TestBase
{
  [Fact]
  public async Task ShouldReturnListOfOnlyThoseWorkspacesThatUserOwns()
  {
    // -- Setup -- //
    var jonsWorkspaceId = await Mediator.Send(
         new CreateWorkspaceCommand(
           WorkspaceName.Validate("Project").OrThrow(),
           Jon
           )
     );

    var danielsWorkspaceId = await Mediator.Send(
         new CreateWorkspaceCommand(
           WorkspaceName.Validate("Project").OrThrow(),
           Daniel
           )
     );
    // -- Act -- //
    var jonsWorkspaces = await Mediator.Send(new ListWorkspacesQuery(Jon));
    var danielsWorkspaces = await Mediator.Send(new ListWorkspacesQuery(Daniel));

    // -- Verify -- //
    jonsWorkspaces.Select(w => w.Id).Should().Contain(jonsWorkspaceId.Value);
    danielsWorkspaces.Select(w => w.Id).Should().Contain(danielsWorkspaceId.Value);
  }

}
