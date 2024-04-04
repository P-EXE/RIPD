using RIPD.Models.ApiConnection;
using System;
using System.Diagnostics;
using System.Threading;

namespace RIPD.Services;

public class APIStatusChecker
{
  private readonly HttpClient _httpClient;

  public APIStatusChecker()
  {
    _httpClient = new();
    _httpClient.Timeout = TimeSpan.FromMilliseconds(2000);
  }
  public async Task<bool> CheckAPI()
  {
    try
    {
      HttpResponseMessage response = await _httpClient.GetAsync(DefaultApiConnection.StatusAddress);
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"==CUSTOM=> APIStatusChecker/CheckAPI: HTTP Request cancelled! \n {ex}");
      return false;
    }
    finally
    {
      _httpClient.Dispose();
    }
  }
}
