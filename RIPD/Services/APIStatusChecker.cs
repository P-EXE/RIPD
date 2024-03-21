using RIPD.Models.ApiConnection;

namespace RIPD.Services;

public class APIStatusChecker
{
  private readonly HttpClient _httpClient;
  private readonly CancellationTokenSource _cancellationTokenSource;

  public APIStatusChecker()
  {
    _httpClient = new();
    _cancellationTokenSource = new();
  }
  public async Task<bool> CheckAPI()
  {
    try
    {
      _cancellationTokenSource.CancelAfter(2000);
      HttpResponseMessage response = await _httpClient.GetAsync(DefaultApiConnection.StatusAddress);
      if (response.IsSuccessStatusCode)
      {
        return true;
      }
      return false;
    }
    catch (OperationCanceledException ex)
    {
      return false;
    }
    finally
    {
      _httpClient.Dispose();
      _cancellationTokenSource.Dispose();
    }
  }
}
