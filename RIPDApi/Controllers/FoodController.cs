using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RIPDApi.Repos;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/food")]
[ApiController]
public class FoodController : ControllerBase
{
  private readonly UserManager<AppUser> _userManager;
  private readonly IFoodRepo _foodRepo;

  public FoodController(UserManager<AppUser> userManager, IFoodRepo foodRepo)
  {
    _userManager = userManager;
    _foodRepo = foodRepo;
  }

  [HttpPost]
  public async Task<Food?> CreateFoodAsync([FromBody] Food_Create createFood)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _foodRepo.CreateFoodAsync(createFood);
  }

  [HttpGet("{id}")]
  public async Task<Food?> GetFoodByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _foodRepo.ReadFoodByIdAsync(id);
  }

  [HttpGet]
  public async Task<IEnumerable<Food>?> GetFoodsByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _foodRepo.ReadFoodsByNameAtPositionAsync(name, position);
  }

  [HttpPut]
  public async Task<Food?> UpdateFoodAsync([FromBody] Food_Update updateFood)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _foodRepo.UpdateFoodAsync(updateFood);
  }

  [HttpDelete("{id}")]
  public async Task<bool> DeleteFoodByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _foodRepo.DeleteFoodByIdAsync(id);
  }
}
