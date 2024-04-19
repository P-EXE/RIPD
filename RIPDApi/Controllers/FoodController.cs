using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/foods")]
[ApiController]
public class FoodController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly UserManager<AppUser> _userManager;
  private readonly SQLDataBaseContext _dbContext;

  public FoodController(IMapper mapper, UserManager<AppUser> userManager, SQLDataBaseContext dbContext)
  {
    _mapper = mapper;
    _userManager = userManager;
    _dbContext = dbContext;
  }

  #region Create

  [HttpPost]
  [Authorize]
  public async Task CreateFoodAsync([FromBody] FoodDTO_Create createFood)
  {
    AppUser? manufacturer = await _dbContext.Users.FindAsync(createFood.ManufacturerId);
    string? userName = User?.Identity?.Name;
    AppUser? contributer = await _dbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
    Food food = new()
    {
      Name = createFood.Name,
      Barcode = createFood.Barcode,
      ManufacturerId = manufacturer.Id,
      Manufacturer = manufacturer,
      ContributerId = contributer.Id,
      Contributer = contributer,
      CreationDateTime = DateTime.Now
    };

    await _dbContext.Foods.AddAsync(food);
    await _dbContext.SaveChangesAsync();
  }

  #endregion Create

  #region Get

  [HttpGet("{foodId}")]
  public async Task<Food?> GetFoodByIdAsync([FromRoute] int foodId)
  {
    Food? food = await _dbContext.Foods
      .FindAsync(foodId);
    return food;
  }

  [HttpGet]
  public async Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync([FromQuery] string foodName, [FromQuery] int position)
  {
    IEnumerable<Food>? foods = _dbContext.Foods
      .Where(f => f.Name.StartsWith(foodName))
      .OrderBy(f => f.Name)
      .Skip(position)
      .Take(5)
      .AsEnumerable();
    return foods;
  }

  #endregion Get
}
