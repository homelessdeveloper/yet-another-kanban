
namespace Yak.Core.Abstractions;

/// <summary>
/// Base class representing an aggregate root in the domain model.
/// </summary>
/// <since>0.0.1</since>
public abstract class AggregateRoot
{
  /// <summary>
  /// List to hold domain events associated with this aggregate root.
  /// </summary>
  internal readonly List<IDomainEvent> Events = new();

  /// <summary>
  /// Retrieves all domain events associated with this aggregate root.
  /// </summary>
  /// <returns>An enumerable collection of domain events.</returns>
  /// <since>0.0.1</since>
  public IEnumerable<IDomainEvent> GetDomainEvents() => Events;

  /// <summary>
  /// Publishes a domain event associated with this aggregate root.
  /// </summary>
  /// <param name="event">The domain event to publish.</param>
  /// <since>0.0.1</since>
  protected internal void PublishEvent(IDomainEvent @event)
  {
    Events.Add(@event);
  }

  /// <summary>
  /// Clears all domain events associated with this aggregate root.
  /// </summary>
  /// <since>0.0.1</since>
  protected internal void ClearEvents()
  {
    Events.Clear();
  }
}

