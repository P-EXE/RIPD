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
  public class FoodDataServiceAPI : IFoodDataService
  {
    private readonly HttpClient _httpClient;
    private readonly string _baseAddress;
    private readonly string _url;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public FoodDataServiceAPI()
    {
      _httpClient = new HttpClient();

      _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? AndroidApiConnection.BaseAddress : WindowsApiConnection.BaseAddress;
      _url = _baseAddress + "foods";

      _jsonSerializerOptions = new JsonSerializerOptions
      {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
      };
    }

    public async Task CreateAsync(Food_CreateDTO food)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.AddFoodAsync: No Internet Access");
        return;
      }

      try
      {
        string jsonFood = JsonSerializer.Serialize<Food_CreateDTO>(food, _jsonSerializerOptions);
        StringContent content = new StringContent(jsonFood, Encoding.UTF8, "application/json");

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

    public Task<Food> GetOneAsync(int id)
    {
      throw new NotImplementedException();
    }

    public Task<Food> GetOneAsync(string barcode)
    {
      throw new NotImplementedException();
    }

    public async Task<List<Food>> GetMultipleAsync()
    {
      List<Food>? foods = new();

      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.GetAllFoodsAsync: No Internet Access");
        return foods;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/all");

        if (response.IsSuccessStatusCode)
        {
          string content = await response.Content.ReadAsStringAsync();
          foods = JsonSerializer.Deserialize<List<Food>>(content, _jsonSerializerOptions);
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): FoodDataService.GetAllFoodsAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): FoodDataService.GetAllFoodsAsync: {ex.Message}");
      }

      return foods;
    }

    public Task<List<Food>> GetMultipleAsync(Dictionary<string, string> queryParams)
    {
      throw new NotImplementedException();
    }

    public async Task UpdateAsync(Food food)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.UpdateFoodAsync: No Internet Access");
        return;
      }

      try
      {
        string jsonFood = JsonSerializer.Serialize<Food>(food, _jsonSerializerOptions);
        StringContent content = new StringContent(jsonFood, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await _httpClient.PutAsync($"{_url}/{food.Id}", content);

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("--> Custom(Success): FoodDataService.UpdateFoodAsync: Updated Food");
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): FoodDataService.UpdateFoodAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): FoodDataService.UpdateFoodAsync: {ex.Message}");
      }
    }

    public async Task DeleteAsync(int id)
    {
      if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
      {
        Debug.WriteLine("--> Custom(Error): FoodDataService.DeleteFoodAsync: No Internet Access");
        return;
      }

      try
      {
        HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/{id}");

        if (response.IsSuccessStatusCode)
        {
          Debug.WriteLine("--> Custom(Success): FoodDataService.DeleteFoodAsync: Deleted Food");
        }
        else
        {
          Debug.WriteLine("--> Custom(Error): FoodDataService.DeleteFoodAsync: Non 2XX http Response");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine($"--> StdEx(Error): FoodDataService.DeleteFoodAsync: {ex.Message}");
      }
    }
  }
}
