using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Yak.Modules.Kanban.FunctionalTests.Infrastructure;

public class FunctionalTestBase : TestEnvironment
{
  protected IMediator Mediator = null!;

  public override async Task InitializeAsync()
  {
    await base.InitializeAsync();

    Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
  }
}