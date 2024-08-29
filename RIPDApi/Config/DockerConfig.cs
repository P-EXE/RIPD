namespace RIPDApi.Config;

public static class DockerConfig
{
  // Default: "Server=host.docker.internal,1433;Database=RIPD;User Id=sa;Password=P455w0rd!;TrustServerCertificate=true;"
  public static string SQLServerConnectionString = Environment.GetEnvironmentVariable("SQLSERVER_SERVER")!;

  // Default: "mongodb://host.docker.internal:27017/"
  public static string MongoServerConnectionString = Environment.GetEnvironmentVariable("MONGOSERVER_SERVER")!;
  // Default: "RIPD"
  public static string MongoServerDatabase = Environment.GetEnvironmentVariable("MONGOSERVER_DATABASE")!;
}
