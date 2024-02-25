using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Testcontainers.PostgreSql;
using Yak.Modules.Identity.Infrastructure;

namespace Yak.Modules.Identity.FunctionalTests.Infrastructure;

public class FunctionalTestBase : IAsyncLifetime
{
  public IServiceProvider ServiceProvider = null!;
  public IServiceScope Scope = null!;
  public IMediator Mediator = null!;

  private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
    .WithDatabase("IntegrationTest")
    .Build();


  public async Task InitializeAsync()
  {
    // We have to start container before we build 
    // ServiceProvider or otherwise we will not be able
    // to register new KanbanDbContext
    await _dbContainer.StartAsync();

    var services = new ServiceCollection();

    services.AddLogging(c => c.AddConsole());

    services.AddTransient<IConfiguration>(_ => new ConfigurationBuilder().Build());
    services.AddIdentityModule();

    services.RemoveAll<DbContextOptions<SecurityDbContext>>();
    services.AddDbContext<SecurityDbContext>(o => o.UseNpgsql(_dbContainer.GetConnectionString()));

    ServiceProvider = services.BuildServiceProvider();
    Scope = ServiceProvider.CreateScope();

    var initializer = Scope.ServiceProvider.GetRequiredService<IdentityDbInitializer>();
    await initializer.InitializeDatabaseAsync();

    Mediator = Scope.ServiceProvider.GetRequiredService<IMediator>();
  }

  public Task DisposeAsync()
  {
    Scope.Dispose();

    // We dont have to clean database cause containers are just "run-and-throw" thing
    // for each test
    return _dbContainer.StopAsync();
  }
}