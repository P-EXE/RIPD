﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
  private readonly IMapper _mapper;
  private readonly SQLDataBaseContext _dbContext;

  public UserController(IMapper mapper, SQLDataBaseContext dbContext)
  {
    _mapper = mapper;
    _dbContext = dbContext;
  }

  [HttpGet("GetSelf"), Authorize]
  public async Task<AppUser?> GetUserName()
  {
    string? userName = User?.Identity?.Name;
    AppUser? appUser = await _dbContext.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
    return appUser;
  }

  #region User

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

  #endregion Worukouts
}
