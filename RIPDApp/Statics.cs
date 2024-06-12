using RIPDShared.Models;

namespace RIPDApp.Statics;

public static class API
{
#if ANDROID
  public static string RouteBaseHttp = "http://10.0.2.2:5115/api/";
  public static string RouteBaseHttps = "https://localhost:7116/api/";
#elif WINDOWS
  public static string RouteBaseHttp = "http://localhost:5115/api/";
  public static string RouteBaseHttps = "https://localhost:7116/api/";
#else
  public static string RouteBaseHttp = string.Empty;
  public static string RouteBaseHttps = string.Empty;
#endif
}

public static class Auth
{
  public static string Email = "user1@mail.com";
  public static string Password = "P455w0rd!";

  public static BearerToken? BearerToken { get; set; }
  public static AppUser? Owner { get; set; }
}