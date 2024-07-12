using RIPDShared.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class WorkoutControllerTests
{
  UserFixture _fixture;

  public WorkoutControllerTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  private readonly Workout_Create _validCreateWorkout;
  public WorkoutControllerTests()
  {
    _validCreateWorkout = new()
    {
      Name = "Valid Workout",
      Description = "A valid Workout for integration testing",
      Energy = 100,
      ContributerId = new()
    };
  }

  [Fact]
  public async void CreateWorkout_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await _fixture.TestClient.PostAsJsonAsync("api/workout", _validCreateWorkout);
    Workout? responseWorkout = JsonSerializer.Deserialize<Workout>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(responseWorkout);
  }
}
