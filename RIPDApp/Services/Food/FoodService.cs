using AutoMapper;
using RIPDShared.Models;

namespace RIPDApp.Services;

public class FoodService : IFoodService
{
  private readonly IHttpService _httpService;
  private readonly IMapper _mapper;
  public FoodService(IHttpService httpService, IMapper mapper)
  {
    _httpService = httpService;
    _mapper = mapper;
  }

  // Needs AutoMapper support
  public async Task<Food?> CreateFoodAsync(Food food)
  {
    // Mapping
    food.Contributer = Statics.Auth.Owner;

    food.ContributerId = Statics.Auth.Owner?.Id;
    food.ManufacturerId = food.Manufacturer?.Id;

    // API
    Food? retFood = await _httpService.PostAsync<Food, Food>("food", food);

    // Return
    return retFood;
  }

  public Task<IEnumerable<Food>?> GetUsersRecentlyAddedFoods()
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync(string query, int position)
  {
    if (string.IsNullOrEmpty(query))
    {
      return null;
    }

    IEnumerable<Food>? foods;
    
    Dictionary<string, string> queries = new()
    {
      ["name"] = query,
      ["position"] = position.ToString(),
    };
    
    foods = await _httpService.GetAsync<IEnumerable<Food>?>("food", queries);
    
    return foods;
  }
}
