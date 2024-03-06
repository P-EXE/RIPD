using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Models.ApiConnection
{
  public static class WindowsApiConnection
  {
    public const string Protocol = "http";
    public const string Domain = "localhost";
    public const string Port = "5203";
    public const string ApiPage = "api";
    public const string BaseAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/";
    public const string StatusPage = "status";
    public const string StatusAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }
  public static class AndroidApiConnection
  {
    public const string Protocol = "http";
    public const string Domain = "10.0.2.2";
    public const string Port = "5203";
    public const string ApiPage = "api";
    public const string BaseAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/";
    public const string StatusPage = "status";
    public const string StatusAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }

  public static class DefaultApiConnection
  {
    public const string Protocol = "http";
    public const string Domain = "192.168.0.201";
    public const string Port = "8080";
    public const string ApiPage = "api";
    public const string BaseAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/";
    public const string StatusPage = "status";
    public const string StatusAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }
}
