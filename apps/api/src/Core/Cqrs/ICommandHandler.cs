using MediatR;

namespace Yak.Core.Cqrs;

/// <summary>
/// The ICommandHandler interface is a public interface used to define a handler for a command in a C# application.
/// It is often used in conjunction with the Command Query Responsibility Segregation (CQRS) pattern,
/// where commands are used to modify the state of the application, and queries are used to retrieve information from the application.
/// </summary>
/// <typeparam name="TCommand">The type of command that the handler can process.</typeparam>
/// <typeparam name="TResponse">The type of result that the handler can return.</typeparam>
/// <since>0.0.1</since>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
  where TCommand : ICommand<TResponse>
{
}
