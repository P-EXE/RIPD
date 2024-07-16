using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Repos;

public class FoodRepo : IFoodRepo
{
  private readonly SQLDataBaseContext _sqlContext;
  private readonly IMapper _mapper;

  // Determines the ammount of Items returned via the Take method
  const int takeSize = 20;

  public FoodRepo(SQLDataBaseContext sqlContext, IMapper mapper)
  {
    _sqlContext = sqlContext;
    _mapper = mapper;
  }

  public async Task<Food?> CreateFoodAsync(Food_Create createFood)
  {
    // Mapping
    Food? food = _mapper.Map<Food>(createFood);
    food.Manufacturer = await _sqlContext.Users.FirstOrDefaultAsync(u => u.Id == food.ManufacturerId);
    food.Contributer = await _sqlContext.Users.FirstOrDefaultAsync(u => u.Id == food.ContributerId);

    // SQL Context
    await _sqlContext.Foods.AddAsync(food);
    await _sqlContext.SaveChangesAsync();
    food = await _sqlContext.Foods
      .Include(f => f.Manufacturer).Include(f => f.Contributer)
      .FirstOrDefaultAsync(f => f.Id == food.Id);

    // Return
    return food;
  }

  public async Task<Food?> ReadFoodByIdAsync(Guid id)
  {
    // SQL Context
    Food? food = await _sqlContext.Foods.FindAsync(id);

    // Return
    return food;
  }

  // Notice: Not awaiting anything
  public async Task<IEnumerable<Food>?> ReadFoodsByNameAtPositionAsync(string name, int position)
  {
    // SQL Context
    // Null reference dereference !!!
    IEnumerable<Food>? foods = _sqlContext.Foods
      .Include(f => f.Manufacturer).Include(f => f.Contributer)
      .Where(f => f.Name.StartsWith(name) || f.Barcode.StartsWith(name))
      .Skip(position * takeSize)
      .Take(takeSize)
      .AsEnumerable();

    // Return
    return foods;
  }

  public async Task<Food?> UpdateFoodAsync(Food_Update updateFood)
  {
    // Mapping
    Food food = _mapper.Map<Food>(updateFood);

    // SQL Context
    _sqlContext.Foods.Update(food);
    await _sqlContext.SaveChangesAsync();

    // Return
    return food;
  }

  public async Task<bool> DeleteFoodByIdAsync(Guid id)
  {
    // SQL Context
    // Possible null reference !!!
    Food? food = await _sqlContext.Foods.FindAsync(id);
    _sqlContext.Foods.Remove(food);
    await _sqlContext.SaveChangesAsync();

    // Return
    return true;
  }
}
