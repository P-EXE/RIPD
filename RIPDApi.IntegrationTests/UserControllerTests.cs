using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

public class UserControllerTests : IntegrationTest
{
  private static readonly AppUser_Create LoginUserValid = new()
  {
    Email = TestUser.Email,
    Password = "P455w0rd!"
  };

  [Fact]
  public async void LogIn_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await TestHttpClient.PostAsJsonAsync("api/user/login", LoginUserValid);
    AppUser? responseUser = JsonSerializer.Deserialize<AppUser>(await response.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }

  [Fact]
  public async void GetUsersByNameAtPositionAsync_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await TestHttpClient.GetAsync($"api/user?name={TestUser.UserName}&position=0");
    IEnumerable<AppUser>? responseUsers = JsonSerializer.Deserialize<IEnumerable<AppUser>>(await response.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }
}
