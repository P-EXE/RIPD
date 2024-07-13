using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class UserControllerTests
{
  UserFixture _fixture;

  public UserControllerTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task Register_Valid()
  {
    // Arrange
    // Act
    HttpResponseMessage response = await _fixture.TestClient.PostAsJsonAsync<Dictionary<string, string>>("/api/user/register", new(){
      { "Email", "registerUser@mail.com" },
      { "Password", "P455w0rd!" }
    });

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
  }

  [Fact]
  public async Task Login_Valid()
  {
    // Act
    HttpResponseMessage response = await _fixture.TestClient.PostAsJsonAsync<Dictionary<string, string>>("/api/user/login", new(){
      { "Email", Defaults.TESTUSER_EMAIL },
      { "Password", Defaults.TESTUSER_PASSWORD }
    });

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    // Act
    BearerToken? bt = JsonSerializer.Deserialize<BearerToken>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(bt);
    Assert.NotNull(bt.TokenType);
    Assert.NotNull(bt.AccessToken);
    Assert.True(0 < bt.ExpiresIn);
    Assert.NotNull(bt.RefreshToken);
  }

  [Fact]
  public async Task Update_Valid()
  {
    // Arrange
    AppUser_Update updateUser = new()
    {
      UserName = "Updated UserName",
      Email = "updated@mail.com"
    };
    
    // Act
    HttpResponseMessage response = await _fixture.TestClient.PutAsJsonAsync("/api/user/manage", updateUser);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);

    // Act
    AppUser? updatedUser = JsonSerializer.Deserialize<AppUser>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(updatedUser);
    Assert.Equal(updateUser.UserName, updatedUser.UserName);
    Assert.Equal(updateUser.Email, updatedUser.Email);
  }
}
