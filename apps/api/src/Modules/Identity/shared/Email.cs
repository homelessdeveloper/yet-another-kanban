using System.Text.RegularExpressions;
using LanguageExt;

namespace Yak.Modules.Identity.Shared;

/// <summary>
/// Represents an email address. This class enforces strong typing and ensures that emails in the domain are always valid.
/// </summary>
/// <since>0.0.1</since>
public class Email : NewType<Email, string>
{
  /// <summary>
  /// Initializes a new instance of the <see cref="Email"/> class with the specified email address.
  /// </summary>
  /// <param name="value">The email address.</param>
  internal Email(string value) : base(value)
  {
  }

  /// <summary>
  /// Creates a new instance of the <see cref="Email"/> class with the specified email address, if it is valid.
  /// </summary>
  /// <param name="email">The email address to create the instance from.</param>
  /// <returns>A validation result containing either a sequence of error messages or a new <see cref="Email"/> instance.</returns>
  public static Validation<string, Email> Validate(string email)
  {
    var value = email.Trim();

    List<string> errors = new List<string>();

    // Check if the email address is empty or consists only of whitespace characters
    if (string.IsNullOrWhiteSpace(email))
    {
      errors.Add("Email address cannot be empty.");
    }

    // Check if the email address is in a valid format using a regular expression
    if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
    {
      errors.Add("Email address is not in a valid format.");
    }

    if (errors.Any()) return errors.ToSeq();

    return new Email(value);
  }
}
