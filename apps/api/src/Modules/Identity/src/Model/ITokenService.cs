using Yak.Modules.Identity.Shared;

namespace Yak.Modules.Identity.Model;

/// <summary>
/// Represents a service for generating authentication tokens.
/// </summary>
/// <since>0.0.1</since>
public interface ITokenService
{
  /// <summary>
  /// Creates an authentication token for the specified principal.
  /// </summary>
  /// <param name="principal">The principal for whom the token is created.</param>
  /// <returns>The generated authentication token.</returns>
  string CreateToken(Principal principal);
}
