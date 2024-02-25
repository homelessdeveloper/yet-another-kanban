using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Testcontainers.PostgreSql;
using Yak.Modules.Kanban.Infrastructure;

namespace Yak.Modules.Kanban.FunctionalTests.Infrastructure;

public class TestEnvironment : IAsyncLifetime
{
  public IServiceProvider ServiceProvider = null!;
  public IServiceScope Scope = null!;

  private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
    .WithDatabase("IntegrationTest")
    .Build();


  public virtual async Task InitializeAsync()
  {
    // We have to start container before we build 
    // ServiceProvider or otherwise we will not be able
    // to register new KanbanDbContext
    await _dbContainer.StartAsync();

    var services = new ServiceCollection();

    services.AddTransient<IConfiguration>(_ => new ConfigurationBuilder().Build());
    services.AddKanbanModule();

    services.RemoveAll<DbContextOptions<KanbanDbContext>>();
    services.AddDbContext<KanbanDbContext>(o => o.UseNpgsql(_dbContainer.GetConnectionString()));

    ServiceProvider = services.BuildServiceProvider();
    Scope = ServiceProvider.CreateScope();

    var initializer = Scope.ServiceProvider.GetRequiredService<KanbanDbInitializer>();
    await initializer.InitializeDatabaseAsync();
  }

  public virtual Task DisposeAsync()
  {
    Scope.Dispose();

    // We dont have to clean database cause containers are just "run-and-throw" thing
    // for each test
    return _dbContainer.StopAsync();
  }
}