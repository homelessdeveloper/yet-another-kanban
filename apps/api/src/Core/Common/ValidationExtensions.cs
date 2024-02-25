using LanguageExt;

namespace Yak.Core.Common;

/// <summary>
/// Provides extension methods for performing validation operations.
/// </summary>
/// <since>0.0.1</since>
public static class ValidationExtensions
{
  /// <summary>
  /// Throws a <see cref="ValidationError"/> if the validation fails, or returns the validated value otherwise.
  /// </summary>
  /// <typeparam name="T">The type of the validated value.</typeparam>
  /// <param name="this">The validation result to process.</param>
  /// <returns>The validated value if the validation succeeds.</returns>
  /// <exception cref="ValidationError">Thrown when the validation fails.</exception>
  /// <since>0.0.1</since>
  public static T OrThrow<T>(this Validation<string, T> @this)
  {
    return @this.Match<T>(
      Succ: (x) => x,
      Fail: (violations) => throw new ValidationError(violations)
    );
  }
}
