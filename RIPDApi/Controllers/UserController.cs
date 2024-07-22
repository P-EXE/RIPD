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
  public async Task<ActionResult<AppUser?>> GetSelfPublic()
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    try
    {
      user = await _userRepo.GetSelfPublicAsync(user);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (user == null) return NotFound(HttpContext.User);
    return Ok(user);
  }

  /// <summary>
  /// Gets the current User from the BearerToken.
  /// </summary>
  /// <returns>The User with all Private information</returns>
  [HttpGet("self/private"), Authorize]
  public async Task<ActionResult<AppUser?>> GetSelfPrivate()
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    try
    {
      user = await _userRepo.GetSelfPrivateAsync(user);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (user == null) return NotFound(HttpContext.User);
    return Ok(user);
  }

  /// <summary>
  /// Search users
  /// </summary>
  /// <returns>The User with all Private information</returns>
  [HttpGet]
  public async Task<ActionResult<IEnumerable<AppUser>?>> GetUsersByNameAtPositionAsync([FromQuery] string name, [FromQuery] int position = 0)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
    IEnumerable<AppUser>? users;

    if (name == null) return BadRequest(name);

    try
    {
      users = await _userRepo.GetUsersByNameAtPosition(name, position);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (users == null) return NotFound(name);
    return Ok(users);
  }

  [HttpPut("manage"), Authorize]
  public async Task<ActionResult<AppUser?>> UpdateSelf([FromBody] AppUser_Update updateUser)
  {
    AppUser? user = await _userManager.GetUserAsync(HttpContext.User);

    if (user == null) return BadRequest(user);
    if (updateUser == null) return BadRequest(updateUser);

    try
    {
      user = await _userRepo.UpdateUserAsync(user, updateUser);
    }
    catch (Exception ex)
    {
      return UnprocessableEntity(ex);
    }

    if (user == null) return NotFound(user);
    return Ok(user);
  }
}
