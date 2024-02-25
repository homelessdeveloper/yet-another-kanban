using Yak.Core.Common;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.Features.Groups.CreateGroup;
using Yak.Modules.Kanban.Features.Workspaces.CreateWorkspace;
using Yak.Modules.Kanban.FunctionalTests.Infrastructure;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban.FunctionalTests.AssignmentManagement;

public class TestBase : FunctionalTestBase
{
  protected Principal Jon = null!;
  protected Principal Daniel = null!;

  protected WorkspaceId JonsWorkspaceId = null!;
  protected WorkspaceId DanielsWorkspaceId = null!;

  protected GroupId JonsToDosGroupId = null!;
  protected GroupId JonsArchiveGroupId = null!;

  protected GroupId DanielsToDosGroupId = null!;
  protected GroupId DanielsArchiveGroupId = null!;


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


    JonsWorkspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Jon's Workspace").OrThrow(),
        Jon
      )
    );

    DanielsWorkspaceId = await Mediator.Send(
      new CreateWorkspaceCommand(
        Name: WorkspaceName.Validate("Daniel's Workspace").OrThrow(),
        Daniel
      )
    );


    JonsToDosGroupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Jon
      )
    );

    JonsArchiveGroupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: JonsWorkspaceId,
        Name: GroupName.Validate("Archive").OrThrow(),
        Principal: Jon
      )
    );


    DanielsToDosGroupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: DanielsWorkspaceId,
        Name: GroupName.Validate("ToDo's").OrThrow(),
        Principal: Daniel
      )
    );

    DanielsArchiveGroupId = await Mediator.Send(
      new CreateGroupCommand(
        WorkspaceId: DanielsWorkspaceId,
        Name: GroupName.Validate("Archive").OrThrow(),
        Principal: Daniel
      )
    );
  }
}
