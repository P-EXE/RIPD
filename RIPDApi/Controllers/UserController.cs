using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDApi.Repos;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly UserManager<AppUser> _userManager;
  private readonly IUserRepo _userRepo;

  public UserController(UserManager<AppUser> userManager, IUserRepo userRepo)
  {
    _userManager = userManager;
    _userRepo = userRepo;
  }

  /// <summary>
  /// Gets the current User from the BearerToken.
  /// </summary>
  /// <returns>The User with all Public information</returns>
  [HttpGet("self/public"), Authorize]
  public async Task<AppUser?> GetSelfPublic()
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _userRepo.GetSelfPublicAsync(user);
  }

  /// <summary>
  /// Gets the current User from the BearerToken.
  /// </summary>
  /// <returns>The User with all Private information</returns>
  [HttpGet("self/private"), Authorize]
  public async Task<AppUser?> GetSelfPrivate()
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _userRepo.GetSelfPrivateAsync(user);
  }

  [HttpGet]
  public async Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _userRepo.GetUsersByNameAtPosition(name, position);
  }

  /* #region User

   [HttpGet("{userId}")]
   public async Task<AppUser?> GetUserAsync([FromRoute] string userId)
   {
     AppUser user = await _dbContext.Users
       .Where(u => u.Id.ToString() == userId).FirstOrDefaultAsync();
     return user;
   }

   [HttpGet]
   public async Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync([FromQuery] string userName, [FromQuery] int position)
   {
     IEnumerable<AppUser>? user = _dbContext.Users
       .Where(u => u.UserName.StartsWith(userName))
       .OrderBy(u => u.UserName)
       .Skip(position)
       .Take(5)
       .AsEnumerable();
     return user;
   }

   #endregion User

   #region Food

   [HttpGet("{userId}/foods/manufactured")]
   public async Task<List<Food>?> GetUserManufacturedFoodsAsync([FromRoute] string userId)
   {
     List<Food> foods = await _dbContext.Foods
       .Where(f => f.ManufacturerId.ToString() == userId).ToListAsync();
     return foods;
   }

   [HttpGet("{userId}/foods/contributed")]
   public async Task<List<Food>?> GetUserContributedFoodsAsync([FromRoute] string userId)
   {
     List<Food> foods = await _dbContext.Foods
       .Where(f => f.ContributerId.ToString() == userId).ToListAsync();
     return foods;
   }

   #endregion Food

   #region Workouts

   [HttpGet("{userId}/workouts/contributed")]
   public async Task<List<Workout>?> GetUserContributedWorkoutsAsync([FromRoute] string userId)
   {
     List<Workout> workouts = await _dbContext.Workouts
       .Where(w => w.ContributerId.ToString() == userId).ToListAsync();
     return workouts;
   }

   #endregion Worukouts*/
}
