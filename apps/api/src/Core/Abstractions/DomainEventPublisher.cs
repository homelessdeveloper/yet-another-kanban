using MediatR;

namespace Yak.Core.Abstractions;

// TODO: Investigate why domain event publisher is not working as a simple alias

/// <summary>
/// Represents a domain event publisher responsible for publishing domain events.
/// </summary>
/// <since>0.0.1</since>
internal record DomainEventPublisher(IMediator Mediator) : IDomainEventPublisher
{

  /// <summary>
  /// Publishes the provided notification.
  /// </summary>
  /// <param name="notification">The notification to publish.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <since>0.0.1</since>
  public Task Publish(object notification, CancellationToken cancellationToken = new CancellationToken())
  {
    return Mediator.Publish(notification, cancellationToken);
  }

  /// <summary>
  /// Publishes the provided notification of type TNotification.
  /// </summary>
  /// <typeparam name="TNotification">The type of notification to publish.</typeparam>
  /// <param name="notification">The notification to publish.</param>
  /// <param name="cancellationToken">The cancellation token.</param>
  /// <returns>A task representing the asynchronous operation.</returns>
  /// <since>0.0.1</since>
  public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = new CancellationToken()) where TNotification : INotification
  {
    return Mediator.Publish(notification, cancellationToken);
  }
}
