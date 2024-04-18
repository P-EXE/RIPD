using RIPDShared.Models;

namespace RIPDApp;

public static class Statics
{
  public static BearerToken? BearerToken { get; set; }
  public static string APIRouteBaseHttp = "http://localhost:5115/api/";
  public static string APIRouteBaseHttps = "https://localhost:7116/api/";
}
