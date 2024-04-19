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
}
