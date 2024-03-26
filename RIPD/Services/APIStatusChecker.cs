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
  }
  public async Task<bool> CheckAPI()
  {
    CancellationTokenSource cancellationTokenSource = new();
    cancellationTokenSource.CancelAfter(1000);
    CancellationToken cancellationToken = cancellationTokenSource.Token;
    try
    {
      HttpResponseMessage response = await _httpClient.GetAsync(DefaultApiConnection.StatusAddress, cancellationToken);
      return response.IsSuccessStatusCode;
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"==CUSTOM=> APIStatusChecker/CheckAPI: HTTP Request cancelled! \n {ex}");
      cancellationTokenSource.Dispose();
      return false;
    }
  }
}
