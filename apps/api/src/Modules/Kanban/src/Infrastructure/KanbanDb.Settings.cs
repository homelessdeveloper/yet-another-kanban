
using System.ComponentModel.DataAnnotations;

namespace Yak.Modules.Kanban.Infrastructure;

/// <summary>
/// Represents the settings for the Kanban database.
/// </summary>
public class KanbanDbSettigns
{
  /// <summary>
  /// The connection string for the Kanban database.
  /// </summary>
  [Required]
  public string ConnectionString { get; init; }
}

