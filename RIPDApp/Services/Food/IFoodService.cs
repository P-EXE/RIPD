using RIPDShared.Models;

namespace RIPDApp.Services;
public interface IFoodService
{
  Task<Food?> CreateFoodAsync(Food food);
  Task<IEnumerable<Food>?> GetUsersRecentlyAddedFoods();
  Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync(string query, int position);
}
