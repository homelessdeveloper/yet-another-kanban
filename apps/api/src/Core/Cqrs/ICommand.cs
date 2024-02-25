using MediatR;

namespace Yak.Core.Cqrs;


/// <summary>
/// The ICommand interface is a public interface used to define a command in a C# application.
/// It is often used in conjunction with the Command Query Responsibility Segregation (CQRS) pattern,
/// where commands are used to modify the state of the application, and queries are used to retrieve information from the application.
/// </summary>
/// <typeparam name="TResult">The type of result that the command is expected to return when it is executed.</typeparam>
/// <since>0.0.1</since>
public interface ICommand<out TResult> : IRequest<TResult>
{
}




