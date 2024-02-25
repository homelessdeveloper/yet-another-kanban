namespace Yak.Core.Abstractions;

/// <summary>
/// Represents a repository for aggregate roots.
/// </summary>
/// <typeparam name="TAggregate">The type of the aggregate root.</typeparam>
/// <since>0.0.1</since>
public class AggregateRepository<TAggregate> where TAggregate : AggregateRoot
{
  /// <summary>
  /// Clears the domain events associated with the specified aggregate.
  /// </summary>
  /// <param name="aggregate">The aggregate for which to clear domain events.</param>
  /// <since>0.0.1</since>
  protected void ClearEvents(TAggregate aggregate) => aggregate.ClearEvents();
}
