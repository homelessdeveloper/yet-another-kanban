using LanguageExt;

namespace Yak.Modules.Identity.Shared;


/// <summary>
/// Represents a password. This class ensures strong typing and immutability.
/// Ensures that users are always assigned a valid password that meets specific criteria.
/// </summary>
/// <remarks>
/// Password must be at least 8 characters long and contain at least one character in upper and lower case,
/// as well as at least one non-alphanumeric character.
/// </remarks>
/// <since>0.0.1</since>
public class Password : NewType<Password, string>
{
  internal Password(string value) : base(value)
  {
  }

  /// <summary>
  /// Creates a new password object from the given string value and validates that it meets the criteria.
  /// </summary>
  /// <param name="password">The password string to create the object from.</param>
  /// <returns>A validation result containing either a list of errors or a valid Password object.</returns>
  public static Validation<string, Password> Validate(string password)
  {
    var errors = new List<string>();
    var value = password.Trim();

    if (string.IsNullOrWhiteSpace(value))
      errors.Add("Password cannot be empty");

    if (value.Length < 8)
      errors.Add("Password must be at least 8 characters long");

    // Check for lowercase character
    if (!value.Any(char.IsLower))
      errors.Add("Password must contain at least one character in lower case");

    // Check for uppercase character
    if (!value.Any(char.IsUpper))
      errors.Add("Password must contain at least one character in upper case");

    // Check for non-alphanumeric character
    if (value.All(char.IsLetterOrDigit))
      errors.Add("Password must contain at least 1 non-alphanumerical character");

    if (errors.Any()) return errors.ToSeq();

    return new Password(value);
  }
}


