using RIPDShared.Models;

namespace RIPDApp.Statics;

public static class API
{
  public static string RouteBaseHttp = "http://localhost:5115/api/";
  public static string RouteBaseHttps = "https://localhost:7116/api/";
  public static BearerToken? BearerToken { get; set; }
}

public static class RegisterLogin
{
  public static string Email = "user1@mail.com";
  public static string Password = "P455w0rd!";
}