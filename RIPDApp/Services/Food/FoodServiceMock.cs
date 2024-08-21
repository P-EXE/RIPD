using RIPDShared.Models;

namespace RIPDApp.Services;

public class FoodServiceMock : IFoodService
{
  public Task<Food?> CreateFoodAsync(Food food)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync(string query, int position)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Food>?> GetUsersRecentlyAddedFoods()
  {
    throw new NotImplementedException();
  }
}
