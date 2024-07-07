using Bogus;
using Microsoft.AspNetCore.Identity;
using RIPDShared.Models;

namespace RIPDApi.IntegrationTests;

public static class IdentitySetup
{
  private static readonly PasswordHasher<AppUser> ph = new();
  public static Task<AppUser> GenerateTestUser()
  {
    Faker<AppUser> userFaker = new Faker<AppUser>()
      .RuleFor(u => u.Id, f => Guid.NewGuid())
      .RuleFor(u => u.UserName, "IntegrationTestUser")
      .RuleFor(u => u.Email, "IntegrationTestUser@mail.com")
      .RuleFor(u => u.EmailConfirmed, true)
      .RuleFor(u => u.PasswordHash, (f, usr) => ph.HashPassword(usr, "P455w0rd!"))
      .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber())
      .RuleFor(u => u.PhoneNumberConfirmed, true)
      .RuleFor(u => u.TwoFactorEnabled, true);

    AppUser testUser = userFaker.GenerateLazy(1).First();
    return Task.FromResult(testUser);
  }
}
