using Yak.Core.Common;
using Yak.Modules.Identity.Shared;
using Yak.Modules.Kanban.FunctionalTests.Infrastructure;

namespace Yak.Modules.Kanban.FunctionalTests.WorkspaceManagement;

public class TestBase : FunctionalTestBase
{
  protected Principal Jon = null!;
  protected Principal Daniel = null!;
  protected Principal Alexander = null!;


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
  }
}