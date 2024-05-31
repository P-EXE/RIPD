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

  public async Task<Food?> CreateFoodAsync(Food food)
  {
    Food_Create createFood = _mapper.Map<Food_Create>(food);
    Food? retFood = await _httpService.PostAsync<Food_Create, Food>("foods", createFood);
    return retFood;
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
