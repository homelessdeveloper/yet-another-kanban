using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Yak.Modules.Identity.Shared
{
  /// <summary>
  /// Provides extension methods for working with HttpContext related to identity.
  /// </summary>
  /// <since>0.0.1</since>
  public static class HttpContextExtensions
  {
    /// <summary>
    /// Retrieves the currently authenticated user from the HttpContext.
    /// </summary>
    /// <returns>The authenticated user's Principal, or null if not authenticated.</returns>
    /// <since>0.0.1</since>
    public static Principal? GetPrincipal(this HttpContext context)
    {
      var userId = context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      var email = context.User.FindFirst(ClaimTypes.Email)?.Value;
      var username = context.User.FindFirst(ClaimTypes.Name)?.Value;

      if (userId is null || username is null || email is null)
        return null;

      return new Principal(
          PrincipalId.New(Guid.Parse(userId)),
          Email.New(email),
          UserName.New(username)
      );
    }

    /// <summary>
    /// Retrieves the currently authenticated user from the HttpContext, throwing an exception if not authenticated.
    /// </summary>
    /// <returns>The authenticated user's Principal.</returns>
    /// <exception cref="SecurityError.AuthenticationError">Thrown if the user is not authenticated.</exception>
    /// <since>0.0.1</since>
    public static Principal RequirePrincipal(this HttpContext context)
    {
      var principal = context.GetPrincipal();

      if (principal is null)
        throw new SecurityError.AuthenticationError();

      return principal;
    }
  }
}
