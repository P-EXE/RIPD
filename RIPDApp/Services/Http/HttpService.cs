using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;

namespace RIPDApp.Services;

/// <summary>
/// Default implementation of the IHTTPService interface.
/// Uses it's own HttpClient which's AuthHeader can be updated via the Authorize Method in an effort to combat socket exhaustion.
/// </summary>
internal class HttpService : IHttpService
{
  private static readonly TimeSpan CancelationTimeout = TimeSpan.FromSeconds(5);
  private readonly HttpClient _httpClient;
  private readonly JsonSerializerOptions _jsonSerializerOptions;
  public HttpService(HttpClient httpClient)
  {
    _httpClient = httpClient;

    _jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
  }

  public async Task<bool> Authorize(AuthenticationHeaderValue authHeader = null)
  {
    // Set the AuthHeader if available
    if (authHeader != null)
    {
      _httpClient.DefaultRequestHeaders.Authorization = authHeader;
      return true;
    }

    // Get the AuthHeader from Statics
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Statics.Auth.BearerToken?.AccessToken ?? "");
    return true;
  }

  public async Task<T?> GetAsync<T>(string route, [CallerMemberName] string caller = "")
  {
    CancellationTokenSource ctSource = new(CancelationTimeout);
    // Request
    HttpResponseMessage response;

    try
    {
      response = await _httpClient.GetAsync(route, ctSource.Token);
    }
    catch (Exception ex)
    {
      throw;
    }

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    T? result;
    try
    {
      result = JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions);
    }
    catch (Exception ex)
    {
      throw;
    }

    return result;
  }

  public async Task<T?> GetAsync<T>(string route, Dictionary<string, object> queriesDict, [CallerMemberName] string caller = "")
  {
    string queries = "?";
    for (int i = 0; i < queriesDict.Count - 1; i++)
    {
      queries += $"{queriesDict.ElementAt(i).Key}={queriesDict.ElementAt(i).Value}&";
    }
    queries += $"{queriesDict.Last().Key}={queriesDict.Last().Value}";

    return await GetAsync<T>(route + queries);
  }

  public async Task<bool> PostAsync<T>(string route, T t, [CallerMemberName] string caller = "")
  {
    CancellationTokenSource ctSource = new(CancelationTimeout);
    // Convert
    string json;
    try
    {
      json = JsonSerializer.Serialize(t, _jsonSerializerOptions);
    }
    catch (NotSupportedException ex)
    {
      throw;
    }

    StringContent content = new(json, Encoding.UTF8, "application/json");

    // Request
    HttpResponseMessage response;
    try
    {
      response = await _httpClient.PostAsync(route, content, ctSource.Token);
    }
    catch (Exception ex)
    {
      throw;
    }

    response.EnsureSuccessStatusCode();

    return true;
  }

  public async Task<T2?> PostAsync<T1, T2>(string route, T1 t1, [CallerMemberName] string caller = "")
  {
    CancellationTokenSource ctSource = new(CancelationTimeout);
    // Convert
    string json;
    try
    {
      json = JsonSerializer.Serialize(t1, _jsonSerializerOptions);
    }
    catch (NotSupportedException ex)
    {
      throw;
    }

    StringContent content = new(json, Encoding.UTF8, "application/json");

    // Request
    HttpResponseMessage response;
    try
    {
      response = await _httpClient.PostAsync(route, content, ctSource.Token);
    }
    catch (Exception ex)
    {
      throw;
    }

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    T2? result;
    try
    {
      result = JsonSerializer.Deserialize<T2>(responseContent, _jsonSerializerOptions);
    }
    catch (Exception ex)
    {
      throw;
    }

    return result;
  }

  public async Task<T2?> PutAsync<T1, T2>(string route, T1 t1, [CallerMemberName] string caller = "")
  {
    CancellationTokenSource ctSource = new(CancelationTimeout);
    // Convert
    string json;
    try
    {
      json = JsonSerializer.Serialize(t1, _jsonSerializerOptions);
    }
    catch (NotSupportedException ex)
    {
      throw;
    }

    StringContent content = new(json, Encoding.UTF8, "application/json");

    // Request
    HttpResponseMessage response;
    try
    {
      response = await _httpClient.PutAsync(route, content, ctSource.Token);
    }
    catch (Exception ex)
    {
      throw;
    }

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    T2? result;
    try
    {
      result = JsonSerializer.Deserialize<T2>(responseContent, _jsonSerializerOptions);
    }
    catch (Exception ex)
    {
      throw;
    }

    return result;
  }

  public async Task<T?> DeleteAsync<T>(string route, Dictionary<string, object> queriesDict, [CallerMemberName] string caller = "")
  {
    string queries = "?";
    for (int i = 0; i < queriesDict.Count - 1; i++)
    {
      queries += $"{queriesDict.ElementAt(i).Key}={queriesDict.ElementAt(i).Value}&";
    }
    queries += $"{queriesDict.Last().Key}={queriesDict.Last().Value}";

    return await DeleteAsync<T>(route + queries);
  }

  public async Task<T?> DeleteAsync<T>(string route, [CallerMemberName] string caller = "")
  {
    CancellationTokenSource ctSource = new(CancelationTimeout);
    // Request
    HttpResponseMessage response;
    try
    {
      response = await _httpClient.DeleteAsync(route, ctSource.Token);
    }
    catch (Exception ex)
    {
      throw;
    }

    response.EnsureSuccessStatusCode();

    string responseContent = await response.Content.ReadAsStringAsync();

    if (responseContent == string.Empty) return await Task.FromResult<T?>(default);

    T? result;
    try
    {
      result = JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions);
    }
    catch (Exception ex)
    {
      throw;
    }

    return result;
  }
}