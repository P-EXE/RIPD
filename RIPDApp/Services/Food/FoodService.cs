using RIPDShared.Models;

namespace RIPDApp.Services;

public class FoodService : IFoodService
{
  private readonly IHttpService _httpService;
  public FoodService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<bool> CreateFoodAsync(FoodDTO_Create createFood)
  {
    return await _httpService.PostAsync("foods", createFood);
  }

  public Task<IEnumerable<Food>?> GetUsersRecentlyAddedFoods()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync(string query, int position)
  {
    IEnumerable<Food>? foods;
    Dictionary<string, string> queries = new()
    {
      ["foodName"] = query,
      ["position"] = position.ToString(),
    };
    foods = await _httpService.GetAsync<IEnumerable<Food>?>("foods", queries);
    return foods;
  }
}
