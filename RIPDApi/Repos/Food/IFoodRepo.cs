using RIPDShared.Models;

namespace RIPDApi.Repos;

public interface IFoodRepo
{
  Task<Food?> CreateFoodAsync(Food_Create createFood);
  Task<Food?> ReadFoodByIdAsync(Guid id);
  Task<IEnumerable<Food>?> ReadFoodsByNameAtPositionAsync(string name, int position);
  Task<Food?> UpdateFoodAsync(Food_Update updateFood);
  Task<bool> DeleteFoodByIdAsync(Guid id);
}
