using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.Models.ApiConnection
{
  internal static class ApiConnection
  {
    public static string Protocol = "http";
    public static string Domain = "localhost";
    public static string Port = "5203";
    public static string ApiPage = "api";
    public static string StatusPage = "status";
    public static string StatusLink = $"{Protocol}://{Domain}:{Port}/{ApiPage}/{StatusPage}";
  }
}
