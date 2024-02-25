using MediatR;

namespace Yak.Core.Abstractions;

/// <summary>
/// Represents a publisher for domain events.
/// </summary>
/// <remarks>
/// Domain event publisher is responsible for publishing domain events to subscribers (handlers).
/// </remarks>
/// <seealso cref="IPublisher" />
/// <since>0.0.1</since>
public interface IDomainEventPublisher : IPublisher
{
}
