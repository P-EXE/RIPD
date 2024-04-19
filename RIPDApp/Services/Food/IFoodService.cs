using RIPDShared.Models;

namespace RIPDApp.Services;
public interface IFoodService
{
  Task<bool> CreateFoodAsync(FoodDTO_Create createFood);
}
