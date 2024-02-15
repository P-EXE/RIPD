using RIPD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RIPD.DataServices
{
  public interface IFoodDataService
  {
    Task AddFoodAsync(Food food);
    Task<List<Food>> GetAllFoodsAsync();
    Task UpdateFoodAsync(Food food);
    Task DeleteFoodAsync(int id);
  }
}
