using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Maui.Controls.Compatibility;
using RIPD.DataServices;
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
  public class UserDataServiceAPI : IUserDataService
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly LocalDBContext _localDBContext;

    public UserDataServiceAPI(LocalDBContext localDBContext)
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

    public async Task CreateAsync(User user)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.AddFoodAsync: No Internet Access");
        return;
      }

      string jsonUser = JsonSerializer.Serialize<User>(user, _jsonSerializerOptions);
      StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

      try
      {
        HttpResponseMessage response = await _httpClient.PostAsync($"{_url}", content);
        switch (response.StatusCode)
        {
          case :
        }
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

    public Task<User> GetAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetAsync(string email)
    {
      throw new NotImplementedException();
    }

    public Task<List<User>> GetMultipleAsync(Dictionary<string, string> queryParams)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(Dictionary<string, string> queryParams)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<User> GetUserAsync(string email)
    {
      throw new NotImplementedException();
    }
  }
}
