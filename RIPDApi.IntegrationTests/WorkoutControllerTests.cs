using RIPDShared.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

public class WorkoutControllerTests : IntegrationTest
{
  private readonly Workout_Create _validCreateWorkout;
  public WorkoutControllerTests()
  {
    _validCreateWorkout = new()
    {
      Name = "Valid Workout",
      Description = "A valid Workout for integration testing",
      Energy = 100,
      ContributerId = TestUser.Id
    };
  }

  [Fact]
  public async void CreateWorkout_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await TestHttpClient.PostAsJsonAsync("api/workout", _validCreateWorkout);
    Workout? responseWorkout = JsonSerializer.Deserialize<Workout>(await response.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(responseWorkout);
  }
}
