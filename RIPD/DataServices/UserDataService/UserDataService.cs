using Microsoft.Maui.Controls.Compatibility;
using RIPD.Models;
using RIPD.Models.ApiConnection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  public class UserDataService
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly LocalDBContext _localDBContext;

    public UserDataService(LocalDBContext localDBContext)
    {
      _localDBContext = localDBContext;
      _httpClient = new HttpClient();
      _baseAddress = _localDBContext.ApiConnections
        .Where(x => x.Active == true)
        .Select(y => y.BaseAddress)
        .FirstOrDefault();

      if (_baseAddress == null)
      {
        _baseAddress = DeviceInfo.Platform == DevicePlatform.WinUI ? WindowsApiConnection.BaseAddress : null;
        _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? AndroidApiConnection.BaseAddress : null;
      }

      _url = _baseAddress + "users";

      _jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };
    }

    public async Task CreateUserAsync(User user)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.AddFoodAsync: No Internet Access");
        return;
      }

      try
      {
        string jsonUser = JsonSerializer.Serialize<User>(user, _jsonSerializerOptions);
        StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PostAsync($"{_url}", content);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("--> Custom(Success): FoodDataService.AddFoodAsync: Added Food");
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): FoodDataService.AddFoodAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): FoodDataService.AddFoodAsync: {ex.Message}");
      }
    }

    public async Task<User> GetUserAsync(string email)
    {
      User user = new();
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): UserDataService.GetUserAsync: No Internet Access");
        return user;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_url}?email={email}");

        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          user = JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): UserDataService.GetUserAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): UserDataService.GetUserAsync: {ex.Message}");
      }

      return user;
    }
    public async Task<User> GetUserAsync(int id)
    {
      User user = new();
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): UserDataService.GetUserAsync: No Internet Access");
        return user;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          user = JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): UserDataService.GetUserAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): UserDataService.GetUserAsync: {ex.Message}");
      }

      return user;
    }


  }
}
