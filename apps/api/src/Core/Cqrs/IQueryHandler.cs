using MediatR;

namespace Yak.Core.Cqrs;

/// <summary>
/// Defines the interface for a query handler that processes queries of type <typeparamref name="TQuery"/>
/// and returns a result of type <typeparamref name="TResponse"/>.
/// </summary>
/// <typeparam name="TQuery">The type of query that this handler can process. The query must implement
/// the <see cref="IQuery{TResponse}"/> interface.</typeparam>
/// <typeparam name="TResponse">The type of result returned by the query.</typeparam>
/// <since>0.0.1</since>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
  where TQuery : IQuery<TResponse>
{
}
