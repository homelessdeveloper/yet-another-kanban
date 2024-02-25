using LanguageExt;

namespace Yak.Modules.Identity.Shared;


/// <summary>
/// Represents a user name value object. This class ensures strong typing and immutability.
/// </summary>
/// <remarks>
/// The user name must be at least 5 characters long and can only contain letters and white spaces.
/// This class guarantees that the user name is always valid and conforms to the business rules of the application.
/// </remarks>
/// <since>0.0.1</since>
public class UserName : NewType<UserName, string>
{
  internal UserName(string value) : base(value)
  {
  }

  /// <summary>
  /// Creates a new instance of the <see cref="UserName"/> class.
  /// </summary>
  /// <param name="userName">The user name to validate and create the value object from.</param>
  /// <returns>A validation result that indicates whether the user name is valid or not.</returns>
  /// <remarks>
  /// The user name must be at least 5 characters long and can only contain letters and white spaces.
  /// </remarks>
  public static Validation<string, UserName> Validate(string userName)
  {
    var value = userName.Trim();

    List<string> errors = new List<string>();

    // Check if the username is empty or consists only of whitespace characters
    if (string.IsNullOrWhiteSpace(value))
    {
      errors.Add("Username cannot be empty.");
    }

    if (value.Length < 5)
    {
      errors.Add("Username must be at least 5 characters long.");
    }

    // Check if the username contains any characters that are not letters or whitespace
    if (value.Any(c => !char.IsLetter(c) && c != ' '))
    {
      errors.Add("Username can only contain letters and whitespaces.");
    }


    if (errors.Any()) return errors.ToSeq();

    return new UserName(value);
  }
}
