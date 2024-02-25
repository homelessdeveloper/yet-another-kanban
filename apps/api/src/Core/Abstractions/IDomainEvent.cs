
using MediatR;

namespace Yak.Core.Abstractions;


/// <summary>
/// Represents a domain event.
/// </summary>
/// <remarks>
/// Domain events are used to represent significant state changes within a domain.
/// </remarks>
/// <since>0.0.1</since>
public interface IDomainEvent : INotification { }

