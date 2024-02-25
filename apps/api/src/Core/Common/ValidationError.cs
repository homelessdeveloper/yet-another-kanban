using LanguageExt;

namespace Yak.Core.Common;

/// <summary>
/// Represents an error that occurs when one or multiple validation checks fails.
/// </summary>
/// <since>0.0.1</since>
public class ValidationError : ApplicationError
{
  public Seq<string> Violations { get; }

  public ValidationError(Seq<string> violations) : base(422, $"One or more validation error(s) happened.\n {violations}")
  {
    Violations = violations;
  }
}
