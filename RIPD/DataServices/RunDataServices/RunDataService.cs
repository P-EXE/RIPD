using RIPD.Models;
using RIPD.Models.ApiConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RIPD.DataServices.RunDataServices
{
  internal class RunDataService : IRunDataService
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _users;
    private readonly string _runs;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    public RunDataService()
    {
      _httpClient = new HttpClient();

      _baseAddress = DeviceInfo.Platform == DevicePlatform.WinUI ? WindowsApiConnection.BaseAddress : null;
      _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? AndroidApiConnection.BaseAddress : null;

      /*_baseAddress = DefaultApiConnection.BaseAddress;*/

      _users = "users";
      _runs = "runs";

      _jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };
    }
    public async Task AddRunAsync(User user, Run run)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): RunDataServices.AddRunsAsync: No Internet Access");
        return;
      }

      try
      {
        string jsonRun = JsonSerializer.Serialize<Run>(run, _jsonSerializerOptions);
        StringContent content = new StringContent(jsonRun, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync($"{_baseAddress}{_users}/{user.Id}/{_runs}/{run.Id}", content);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("--> Custom(Success): RunDataServices.AddRunsAsync: Added Run");
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): RunDataServices.AddRunsAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): RunDataService.AddRunsAsync: {ex.Message}");
      }
    }

    public async Task<List<Run>> GetAllRunsByUserAsync(User user)
    {
      List<Run> runs = new List<Run>();

      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): RunDataService.GetAllRunsAsync: No Internet Access");
        return runs;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_users}/{user.Id}{_runs}/all");

        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          runs = JsonSerializer.Deserialize<List<Run>>(content, _jsonSerializerOptions);
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): RunDataService.GetAllRunAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): RunDataService.GetAllRunAsync: {ex.Message}");
      }

      return runs;
    }

    public async Task<Run> GetRunAsync(User user, int id)
    {
      Run run;

      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): RunDataService.GetOneRunAsync: No Internet Access");
        return null;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_users}/{user.Id}{_runs}/{id}");

        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          run = JsonSerializer.Deserialize<Run>(content, _jsonSerializerOptions);
          return run;
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): RunDataService.GetOneRunAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): RunDataService.GetOneRunAsync: {ex.Message}");
        return null;
      }
      return null;
    }
  }
}
