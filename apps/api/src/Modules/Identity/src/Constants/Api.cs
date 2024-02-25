namespace Yak.Modules.Identity.Constants;

/// <summary>
/// Represents a collection of constants and configurations related to the API.
/// </summary>
/// <since>0.0.1</since>
public static class Api
{
  /// <summary>
  /// Contains tag constants used for API documentation.
  /// </summary>
  public static class Tags
  {
    /// <summary>
    /// The tag for authentication-related endpoints.
    /// </summary>
    public const string Auth = "Auth";
  }

  /// <summary>
  /// Contains path constants used for API routing.
  /// </summary>
  public static class Paths
  {
    /// <summary>
    /// The base path for API endpoints.
    /// </summary>
    public const string Base = "Api";

    /// <summary>
    /// The path for authentication-related endpoints.
    /// </summary>
    public const string Auth = $"{Base}/Auth";
  }
}

