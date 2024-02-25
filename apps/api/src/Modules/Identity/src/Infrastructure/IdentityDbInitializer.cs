using Microsoft.EntityFrameworkCore;

namespace Yak.Modules.Identity.Infrastructure;

/// <summary>
/// Represents an initializer for the identity database.
/// </summary>
/// <param name="SecurityDbContext">The security database context.</param>
/// <since>0.0.1</since>
public record IdentityDbInitializer(SecurityDbContext SecurityDbContext)
{
  /// <summary>
  /// Initializes the identity database asynchronously.
  /// </summary>
  /// <returns>A task representing the asynchronous operation.</returns>
  public async Task InitializeDatabaseAsync()
  {
    // Apply identity database migrations
    await SecurityDbContext.Database.MigrateAsync();
  }
}
