using MediatR;

namespace Yak.Core.Abstractions;

/// <summary>
/// Represents a handler for domain events of type TEvent.
/// </summary>
/// <typeparam name="TEvent">The type of domain event to handle.</typeparam>
/// <remarks>
/// Domain event handlers are responsible for reacting to domain events and executing appropriate actions.
/// </remarks>
/// <since>0.0.1</since>
public interface IDomainEventHandler<in TEvent> : INotificationHandler<TEvent>
  where TEvent : IDomainEvent
{
}
