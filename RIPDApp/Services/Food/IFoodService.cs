using RIPDShared.Models;

namespace RIPDApp.Services;
public interface IFoodService
{
  Task<bool> CreateFoodAsync(FoodDTO_Create createFood);
  Task<IEnumerable<Food>?> GetUsersRecentlyAddedFoods();
  Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync(string query, int position);
}
