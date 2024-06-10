using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RIPDApi.Data;
using RIPDShared.Models;

namespace RIPDApi.Repos;

public class UserRepo : IUserRepo
{
  private readonly SQLDataBaseContext _sqlContext;
  private readonly IMapper _mapper;
  private const int takeSize = 20;

  public UserRepo(SQLDataBaseContext sqlContext, IMapper mapper)
  {
    _sqlContext = sqlContext;
    _mapper = mapper;
  }

  public async Task<AppUser?> GetSelfPrivateAsync(AppUser? user)
  {
    if (user == null) return null;

    // SQL Context
    user = await _sqlContext.Users.FindAsync(user.Id);
    user.Diary = await _sqlContext.Diaries.FindAsync(user.Id);

    // Return
    return user;
  }

  public async Task<AppUser?> GetSelfPublicAsync(AppUser? findUser)
  {
    if (findUser == null) return null;
    AppUser? retUser = await _sqlContext.Users.FindAsync(findUser.Id);
    return retUser;
  }

  public async Task<IEnumerable<AppUser>?> GetUsersByNameAtPosition(string name, int position)
  {
    // SQL Context
    IEnumerable<AppUser>? users = _sqlContext.Users
      .Where(u => u.UserName.StartsWith(name))
      .Skip(position * takeSize)
      .Take(takeSize)
      .AsEnumerable();

    // Return
    return users;
  }
}
