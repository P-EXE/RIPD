using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Models.ApiConnection
{
  public static class WindowsApiConnection
  {
    public static string Protocol = "http";
    public static string Domain = "localhost";
    public static string Port = "5203";
    public static string ApiPage = "api";
    public static string BaseAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/";
    public static string StatusPage = "status";
    public static string StatusAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }
  public static class AndroidApiConnection
  {
    public static string Protocol = "http";
    public static string Domain = "10.0.2.2";
    public static string Port = "5203";
    public static string ApiPage = "api";
    public static string BaseAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/";
    public static string StatusPage = "status";
    public static string StatusAddress = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }
}
