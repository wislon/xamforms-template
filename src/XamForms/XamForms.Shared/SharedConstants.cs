namespace XamForms.Shared
{
  public static class SharedConstants
  {
    /// <summary>
    /// Root url of your base API endpoint.
    /// </summary>
    public static string ApiBaseUrl => $"{GetSiteBaseBaseUrl()}api/";

    public const string SharedAkavacheCacheName = "LGIACache";

    /// <summary>
    /// Calculated for now because you may have different ones depending 
    /// on your location or requirements.
    /// </summary>
    /// <returns></returns>
    public static string GetSiteBaseBaseUrl()
    {
      // return $"https://yoursite.azurewebsites.net/";
      return "your site base url goes here";
    }
  }
}