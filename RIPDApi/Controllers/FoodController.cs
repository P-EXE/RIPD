using Microsoft.AspNetCore.Authorization;
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
  [Authorize]
  public async Task<ActionResult<Food?>> CreateFoodAsync([FromBody] Food_Create createFood)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Food? food = null;

    if (createFood == null) return BadRequest(createFood);

    try
    {
      food = await _foodRepo.CreateFoodAsync(createFood);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (food == null) return NotFound(food);
    return Created(nameof(CreateFoodAsync), food);
  }

  [HttpGet("{id}")]
  public async Task<ActionResult<Food?>> GetFoodByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Food? food = null;

    if (id == default)
    {
      return BadRequest(id);
    }

    food = await _foodRepo.ReadFoodByIdAsync(id);

    return food == null ? NotFound(food) : Ok(food);
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<Food>?>> GetFoodsByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position = 0)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<Food>? foods = null;

    if (name == null) return BadRequest(name);

    foods = await _foodRepo.ReadFoodsByNameAtPositionAsync(name, position);

    return foods!.Any() ? Ok(foods) : NotFound(foods);
  }

  [HttpPut]
  [Authorize]
  public async Task<ActionResult<Food?>> UpdateFoodAsync([FromBody] Food_Update updateFood)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    Food? food = null;

    if (updateFood == null) return BadRequest(food);

    try
    {
      food = await _foodRepo.UpdateFoodAsync(updateFood);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return food == null ? Conflict(updateFood) : Ok(food);
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<bool>> DeleteFoodByIdAsync([FromRoute] Guid id)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    bool success = false;

    if (id == default) return BadRequest(id);

    try
    {
      success = await _foodRepo.DeleteFoodByIdAsync(id);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    return success ? Ok(success) : NotFound(id);
  }
}
