
namespace Yak.Core.Common;

/// <summary>
/// Represents an application error.
/// </summary>
/// <remarks>
/// Application errors encapsulate errors that occur within the application domain.
/// </remarks>
/// <since>0.0.1</since>
public abstract class ApplicationError : Exception
{
  /// <summary>
  /// Gets the error code associated with the application error.
  /// </summary>
  /// <value>The error code.</value>
  /// <since>0.0.1</since>
  public int Code { get; }

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationError"/> class with the specified message and error code.
  /// </summary>
  /// <param name="message">The error message.</param>
  /// <param name="code">The error code.</param>
  /// <since>0.0.1</since>
  protected ApplicationError(string message, int code) : base(message)
  {
    Code = code;
  }

  /// <summary>
  /// Initializes a new instance of the <see cref="ApplicationError"/> class with the specified message and error code.
  /// </summary>
  /// <param name="code">The error code.</param>
  /// <param name="message">The error message.</param>
  /// <since>0.0.1</since>
  protected ApplicationError(int code, string message) : base(message)
  {
    Code = code;
  }
}