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

  [Fact]
  public async void Workout_Create_Valid()
  {
    // Arrange
    Workout_Create create = new()
    {
      Name = "Integration test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Integration test Workout Description",
      Energy = 100
    };

    // Act
    HttpResponseMessage response = await _fixture.TestClient.PostAsJsonAsync("api/workout", create);
    Workout? created = JsonSerializer.Deserialize<Workout>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(created);
    Assert.Equal(_fixture.TestUser.Id, created.Contributer?.Id);
  }

  [Fact]
  public async void Workout_GetById_ValidEmpty()
  {
    // Arrange
    Guid id = new("00000000-0000-0000-0000-000000000001");

    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync($"api/workout/{id}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async void Workout_GetById_Valid()
  {
    // Arrange
    Workout_Create create = new()
    {
      Name = "Integration test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Integration test Workout Description",
      Energy = 100
    };

    HttpResponseMessage responseCreated = await _fixture.TestClient.PostAsJsonAsync("api/workout", create);
    Workout? created = JsonSerializer.Deserialize<Workout>(await responseCreated.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.NotNull(created);

    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync($"api/workout/{created.Id}");
    Workout? found = JsonSerializer.Deserialize<Workout>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(found);
    Assert.Equal(created.Id, found.Id);
  }

  [Fact]
  public async void Workout_GetByNameAtPosition_ValidEmpty()
  {
    // Arrange
    string name = "name";
    int position = 0;

    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync($"api/workout?name={name}&position={position}");
    IEnumerable<Workout>? read = JsonSerializer.Deserialize<IEnumerable<Workout>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    Assert.Equal([], read);
  }

  [Fact]
  public async void Workout_GetByNameAtPosition_Valid()
  {
    // Arrange
    Workout_Create create = new()
    {
      Name = "Integration test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Integration test Workout Description",
      Energy = 100
    };

    int position = 0;

    HttpResponseMessage responseCreate = await _fixture.TestClient.PostAsJsonAsync("api/workout", create);
    Workout? created = JsonSerializer.Deserialize<Workout>(await responseCreate.Content.ReadAsStringAsync(), _fixture.JsonOpt);
    
    Assert.Equal(HttpStatusCode.Created, responseCreate.StatusCode);
    Assert.NotNull(created);

    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync($"api/workout?name={created.Name}&position={position}");
    IEnumerable<Workout>? read = JsonSerializer.Deserialize<IEnumerable<Workout>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(read);
    Assert.NotEmpty(read);
  }

  [Fact]
  public async void Workout_DeleteById_Valid()
  {
    // Arrange
    Workout_Create create = new()
    {
      Name = "Integration test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Integration test Workout Description",
      Energy = 100
    };

    HttpResponseMessage responseCreate = await _fixture.TestClient.PostAsJsonAsync("api/food", create);
    Workout? created = JsonSerializer.Deserialize<Workout>(await responseCreate.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreate.StatusCode);
    Assert.NotNull(created);

    // Act
    HttpResponseMessage response = await _fixture.TestClient.DeleteAsync($"api/food/{created?.Id}");
    bool deleted = JsonSerializer.Deserialize<bool>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.True(deleted);
  }
}
