using MediatR;

namespace Yak.Core.Cqrs;

/// <summary>
/// Defines the interface for a query that returns a result of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TResponse">The type of result returned by the query.</typeparam>
/// <since>0.0.1</since>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}
