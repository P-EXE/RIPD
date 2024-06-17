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

  /// <summary>
  /// Search users
  /// </summary>
  /// <returns>The User with all Private information</returns>
  [HttpGet]
  public async Task<IEnumerable<AppUser>?> GetUsersByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position = 0)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _userRepo.GetUsersByNameAtPosition(name, position);
  }

  [HttpPut("manage"), Authorize]
  public async Task<AppUser?> UpdateSelf([FromBody] AppUser_Update updateUser)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    return await _userRepo.UpdateUserAsync(updateUser);
  }
}
