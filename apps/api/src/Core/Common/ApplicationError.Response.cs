namespace Yak.Core.Common
{
  /// <summary>
  /// Represents an error response returned by the application.
  /// </summary>
  /// <remarks>
  /// This record is used to encapsulate error information including the HTTP status code and a message.
  /// </remarks>
  /// <since>0.0.1</since>
  public record ApplicationErrorResponse
  {
    /// <summary>
    /// Gets the HTTP status code of the error response.
    /// </summary>
    public int StatusCode { get; init; }

    /// <summary>
    /// Gets the error message associated with the error response.
    /// </summary>
    public string Message { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationErrorResponse"/> record.
    /// </summary>
    /// <param name="statusCode">The HTTP status code of the error response.</param>
    /// <param name="message">The error message associated with the error response.</param>
    public ApplicationErrorResponse(int statusCode, string message)
    {
      StatusCode = statusCode;
      Message = message;
    }
  }
}
