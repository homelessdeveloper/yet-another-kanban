using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Yak.Core.Abstractions;
using Yak.Modules.Kanban.Infrastructure;
using Yak.Modules.Kanban.Model;

namespace Yak.Modules.Kanban;

/// <summary>
/// Serves as the marker for the current Assembly reference
/// </summary>
/// <since> 0.0.1 </since>
internal class KanbanModule { }

public static class Extensions
{
  /// <summary>
  /// Configures services required for the Kanban module.
  /// </summary>
  /// <param name="services">The collection of services to add to.</param>
  /// <since>0.0.1</since>
  public static void AddKanbanModule(this IServiceCollection services)
  {
    var provider = services.BuildServiceProvider();
    var configuration = provider.GetRequiredService<IConfiguration>();

    services.AddAbstractionsModule();
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(KanbanModule).Assembly));

    services.AddMvc()
      .AddApplicationPart(typeof(KanbanModule).Assembly)
      .AddControllersAsServices();

    services.AddOptions<KanbanDbSettigns>()
      .Bind(configuration.GetSection("Kanban:Datasource"))
      .ValidateDataAnnotations()
      .ValidateOnStart();

    services.AddScoped<KanbanDbInitializer>();
    services.AddScoped<IWorkspaceStore, WorkspaceStore>();
    services.AddDbContext<KanbanDbContext>(o =>
    {
      var cfg = configuration.GetSection("Kanban:Datasource").Get<KanbanDbSettigns>()!;
      o.UseNpgsql(cfg.ConnectionString);
    });
  }

  /// <summary>
  /// Initializes and configures the Kanban module for use in the application.
  /// </summary>
  /// <param name="app">The web application builder.</param>
  /// <since>0.0.1</since>
  public static async Task UseKanbanModule(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var initializer = scope.ServiceProvider.GetRequiredService<KanbanDbInitializer>();
    await initializer.InitializeDatabaseAsync();
  }
}


