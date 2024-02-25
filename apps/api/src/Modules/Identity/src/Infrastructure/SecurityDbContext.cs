using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Yak.Core.Common;
using Yak.Modules.Identity.Model;
using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Infrastructure;

/// <summary>
/// Represents the database context for security-related entities.
/// </summary>
/// <since>0.0.1</since>
public class SecurityDbContext : IdentityDbContext<SecurityUser, IdentityRole<UserId>, UserId>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="SecurityDbContext"/> class.
  /// </summary>
  public SecurityDbContext()
  {
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="SecurityDbContext"/> class with the specified options.
  /// </summary>
  /// <param name="options">The options for configuring the context.</param>
  public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
  {
  }

  /// <inheritdoc/>
  protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
  {
    base.ConfigureConventions(configurationBuilder);

    configurationBuilder
      .Properties<UserId>()
      .HaveConversion<NewTypeConverter<UserId, Guid>>();
  }

  /// <inheritdoc/>
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    base.OnConfiguring(optionsBuilder);

    optionsBuilder.UseNpgsql(); // Configuring PostgreSQL as the database provider
  }
}

