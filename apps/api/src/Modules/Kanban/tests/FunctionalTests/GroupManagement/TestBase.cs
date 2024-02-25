using Yak.Core.Common;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.FunctionalTests.Infrastructure;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.GroupManagement;

public class TestBase : FunctionalTestBase
{
  protected Principal Jon = null!;
  protected Principal Daniel = null!;
  protected Principal Alexander = null!;

  protected WorkspaceId JonsWorkspaceId = null!;
  protected WorkspaceId DanielsWorkspaceId = null!;
  protected WorkspaceId AlexandrsWorkspaceId = null!;

  public override async Task InitializeAsync()
  {
    await base.InitializeAsync();

    Jon = new Principal(
      Id: PrincipalId.Random(),
      UserName: UserName.Validate("Jonathan").OrThrow(),
      Email: Email.Validate("jonathan@mail.com").OrThrow()
    );

    Daniel = new Principal(
      Id: PrincipalId.Random(),
      UserName: UserName.Validate("Daniel").OrThrow(),
      Email: Email.Validate("daniel@mail.com").OrThrow()
    );

    Alexander = new Principal(
      Id: PrincipalId.Random(),
      UserName: UserName.Validate("Alexander").OrThrow(),
      Email: Email.Validate("alexander@mail.com").OrThrow()
    );


    JonsWorkspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Jon's Workspace").OrThrow(),
        Jon
      )
    );

    DanielsWorkspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Daniel's Workspace").OrThrow(),
        Jon
      )
    );

    AlexandrsWorkspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Alexandr's Workspace").OrThrow(),
        Jon
      )
    );
  }
}