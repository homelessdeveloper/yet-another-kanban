using Microsoft.EntityFrameworkCore;
using Yak.Core.Common;
using Yak.Modules.Kanban.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Kanban.Infrastructure;

/// <summary>
/// Represents the database context for the Kanban application.
/// </summary>
public class KanbanDbContext : DbContext
{

  /// <summary>
  /// Initializes a new instance of the <see cref="KanbanDbContext"/> class with the specified options.
  /// </summary>
  /// <param name="options">The options for this context.</param>
  public KanbanDbContext(DbContextOptions<KanbanDbContext> options) : base(options)
  {
  }

  /// <summary>
  /// Gets or sets the DbSet of Workspaces.
  /// </summary>
  public DbSet<Workspace> Workspaces => Set<Workspace>();

  /// <summary>
  /// Gets or sets the DbSet of Groups.
  /// </summary>
  public DbSet<Group> Groups => Set<Group>();

  /// <summary>
  /// Gets or sets the DbSet of Assignments.
  /// </summary>
  public DbSet<Assignment> Assignments => Set<Assignment>();

  /// <summary>
  /// Configures the model schema and conventions for the database context.
  /// </summary>
  /// <param name="configurationBuilder">The model configuration builder.</param>
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    base.ConfigureConventions(configurationBuilder);

    configurationBuilder
      .Properties<UserId>()
      .HaveConversion<NewTypeConverter<UserId, Guid>>();

    configurationBuilder
      .Properties<UserName>()
      .HaveConversion<NewTypeConverter<UserName, string>>();

    configurationBuilder
      .Properties<Password>()
      .HaveConversion<NewTypeConverter<Password, string>>();

    configurationBuilder
      .Properties<WorkspaceId>()
      .HaveConversion<NewTypeConverter<WorkspaceId, Guid>>();

    configurationBuilder
      .Properties<WorkspaceName>()
      .HaveConversion<NewTypeConverter<WorkspaceName, string>>();

    configurationBuilder
      .Properties<GroupId>()
      .HaveConversion<NewTypeConverter<GroupId, Guid>>();

    configurationBuilder
      .Properties<GroupName>()
      .HaveConversion<NewTypeConverter<GroupName, string>>();

    configurationBuilder
      .Properties<AssignmentId>()
      .HaveConversion<NewTypeConverter<AssignmentId, Guid>>();

    configurationBuilder
      .Properties<AssignmentTitle>()
      .HaveConversion<NewTypeConverter<AssignmentTitle, string>>();
  }

  /// <summary>
  /// Configures the model schema for the database context.
  /// </summary>
  /// <param name="builder">The model builder used to construct the model for this context.</param>
  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Workspace>()
      .HasKey(s => s.Id);

    builder.Entity<Workspace>()
      .Ignore((w) => w.Assignments);

    builder.Entity<Workspace>()
      .HasMany(e => e.Groups)
      .WithOne(e => e.Workspace)
      .HasForeignKey(e => e.WorkspaceId)
      .HasPrincipalKey(e => e.Id);
  }

  /// <summary>
  /// Configures the database context options.
  /// </summary>
  /// <param name="optionsBuilder">The options builder used to configure the context.</param>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseNpgsql();
  }
}
