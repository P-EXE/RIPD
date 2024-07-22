using Microsoft.AspNetCore.Identity;
using RIPDShared.Models;
using Bogus;

namespace RIPDApi.Data;

public class Seeding
{
  private static readonly PasswordHasher<AppUser> ph = new();

  public static IEnumerable<AppUser> GenerateFakeAndTestUser(int fakeUsersCount)
  {
    List<AppUser> users = [];
    users.Add(GenerateTestUser());
    users.AddRange(GenerateFakeUsers(fakeUsersCount));
    return users;
  }

  public static IEnumerable<AppUser> GenerateFakeUsers(int fakeUsersCount)
  {
    Faker<AppUser> userFaker = new Faker<AppUser>()
      .RuleFor(u => u.Id, f => Guid.NewGuid())
      .RuleFor(u => u.UserName, f => f.Name.FirstName())
      .RuleFor(u => u.Email, (f, usr) => f.Internet.Email(usr.UserName, f.Name.LastName()))
      .RuleFor(u => u.EmailConfirmed, true)
      .RuleFor(u => u.PasswordHash, (f, usr) => ph.HashPassword(usr, "P455w0rd!"))
      .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
      .RuleFor(u => u.PhoneNumberConfirmed, true)
      .RuleFor(u => u.TwoFactorEnabled, true);

    return userFaker.GenerateLazy(fakeUsersCount).ToList();
  }

  public static AppUser GenerateTestUser()
  {
    Faker<AppUser> userFaker = new Faker<AppUser>()
      .RuleFor(u => u.Id, f => new("00000000-0000-0000-0000-000000000001"))
      .RuleFor(u => u.UserName, f => f.Name.FirstName())
      .RuleFor(u => u.Email, (f, usr) => f.Internet.Email(usr.UserName, f.Name.LastName()))
      .RuleFor(u => u.EmailConfirmed, true)
      .RuleFor(u => u.PasswordHash, (f, usr) => ph.HashPassword(usr, "P455w0rd!"))
      .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
      .RuleFor(u => u.PhoneNumberConfirmed, true)
      .RuleFor(u => u.TwoFactorEnabled, true);

    return userFaker.GenerateLazy(1).First();
  }
}