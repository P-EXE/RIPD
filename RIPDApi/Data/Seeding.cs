using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using RIPDShared.Models;

namespace RIPDApi.Data;

public class Seeding
{
  private const int defaultCount = 9;
  private static PasswordHasher<AppUser> ph = new();
  private readonly SQLDataBaseContext _sqlContext;
  private readonly UserManager<AppUser> _userManager;

  public Seeding(SQLDataBaseContext sqlContext, UserManager<AppUser> userManager)
  {
    _sqlContext = sqlContext;
    _userManager = userManager;
  }

  public async void GenerateAppUsers(int count = defaultCount)
  {
    for (int i = 1; i <= count; i++)
    {
      AppUser user = new()
      {
        Id = new($"11111111-1111-1111-1111-11111111111{i}"),
        UserName = $"seededUser{i}",
        NormalizedUserName = $"USER{i}",
        Email = $"seededUser{i}@mail.com",
        NormalizedEmail = $"SEEDEDUSER{i}@MAIL.COM",
        EmailConfirmed = true
      };

      await _userManager.CreateAsync(user);
    }
  }

  public async Task<List<Diary>> GenerateDiaries(int count = defaultCount)
  {
    List<Diary> diaries = [];

    for (int i = 1; i <= count; i++)
    {
      diaries.Add(new()
      {
        Id = new($"22222222-2222-2222-2222-22222222222{i}"),
        OwnerId = new($"11111111-1111-1111-1111-11111111111{i}")
      });
    }

    return diaries;
  }
}