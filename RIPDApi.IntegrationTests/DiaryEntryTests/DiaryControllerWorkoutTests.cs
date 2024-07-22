using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class DiaryControllerWorkoutEntryTests
{
  private readonly UserFixture _fixture;

  public DiaryControllerWorkoutEntryTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task EntryWorkout_WithWorkout_Create_Valid()
  {
    // Arrange
    Workout_Create createWorkout = new()
    {
      Name = "Workout Entry test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Workout Entry test Workout Description",
      Energy = 100
    };

    HttpResponseMessage responseCreatedWorkout = await _fixture.TestClient.PostAsJsonAsync("api/workout", createWorkout, _fixture.JsonOpt);
    Workout? createdWorkout = JsonSerializer.Deserialize<Workout>(await responseCreatedWorkout.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedWorkout.StatusCode);
    Assert.NotNull(createdWorkout);

    DiaryEntry_Workout_Create createEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      WorkoutId = createdWorkout.Id,
      Amount = 100
    };

    // Act
    HttpResponseMessage responseCreateEntry = await _fixture.TestClient.PostAsJsonAsync("api/diary/workout", createEntry, _fixture.JsonOpt);
    DiaryEntry_Workout? createdEntry = JsonSerializer.Deserialize<DiaryEntry_Workout>(await responseCreateEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, responseCreateEntry.StatusCode);
    Assert.NotNull(createdEntry);
  }

  [Fact]
  public async Task EntryWorkout_Get_FromToDate_ValidEmpty()
  {
    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/workout");
    IEnumerable<DiaryEntry_Workout>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_Workout>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
  }

  [Fact]
  public async Task EntryWorkout_Get_FromToDate_Valid()
  {
    // Arrange
    await EntryWorkout_WithWorkout_Create_Valid();

    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/workout");
    IEnumerable<DiaryEntry_Workout>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_Workout>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
    Assert.True(got.Any());
  }

  [Fact]
  public async Task EntryWorkout_CreateAndUpdate_Valid()
  {
    // Arrange
    Workout_Create createWorkout = new()
    {
      Name = "Workout Entry test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Workout Entry test Workout Description",
      Energy = 100
    };

    HttpResponseMessage responseCreatedWorkout = await _fixture.TestClient.PostAsJsonAsync("api/workout", createWorkout, _fixture.JsonOpt);
    Workout? createdWorkout = JsonSerializer.Deserialize<Workout>(await responseCreatedWorkout.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedWorkout.StatusCode);
    Assert.NotNull(createdWorkout);

    DiaryEntry_Workout_Create createEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      WorkoutId = createdWorkout.Id,
      Amount = 100
    };

    HttpResponseMessage responseCreatedEntry = await _fixture.TestClient.PostAsJsonAsync("api/diary/workout", createEntry, _fixture.JsonOpt);
    DiaryEntry_Workout? createdEntry = JsonSerializer.Deserialize<DiaryEntry_Workout>(await responseCreatedEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedEntry.StatusCode);
    Assert.NotNull(createdEntry);

    DiaryEntry_Workout_Update updateEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      EntryNr = createdEntry.EntryNr,
      Acted = DateTime.UtcNow,
      WorkoutId = createdWorkout.Id ?? new(),
      Amount = 101
    };

    // Act
    HttpResponseMessage responseUpdatedEntry = await _fixture.TestClient.PutAsJsonAsync("api/diary/workout", updateEntry, _fixture.JsonOpt);
    DiaryEntry_Workout? updatedEntry = JsonSerializer.Deserialize<DiaryEntry_Workout>(await responseUpdatedEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, responseUpdatedEntry.StatusCode);
    Assert.NotNull(updatedEntry);
    Assert.Equal(createdEntry.EntryNr, updatedEntry.EntryNr);
    Assert.NotEqual(createdEntry.Amount, updatedEntry.Amount);
  }

  [Fact]
  public async Task EntryWorkout_CreateAndDelete_Valid()
  {
    // Arrange
    Workout_Create createWorkout = new()
    {
      Name = "Workout Entry test Workout Name",
      ContributerId = _fixture.TestUser.Id,
      Description = "Workout Entry test Workout Description",
      Energy = 100
    };

    HttpResponseMessage responseCreatedWorkout = await _fixture.TestClient.PostAsJsonAsync("api/workout", createWorkout, _fixture.JsonOpt);
    Workout? createdWorkout = JsonSerializer.Deserialize<Workout>(await responseCreatedWorkout.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedWorkout.StatusCode);
    Assert.NotNull(createdWorkout);

    DiaryEntry_Workout_Create createEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      WorkoutId = createdWorkout.Id,
      Amount = 100
    };

    HttpResponseMessage responseCreatedEntry = await _fixture.TestClient.PostAsJsonAsync("api/diary/workout", createEntry, _fixture.JsonOpt);
    DiaryEntry_Workout? createdEntry = JsonSerializer.Deserialize<DiaryEntry_Workout>(await responseCreatedEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedEntry.StatusCode);
    Assert.NotNull(createdEntry);

    // Act
    HttpResponseMessage responseDelete = await _fixture.TestClient.DeleteAsync($"api/diary/workout?entry={createdEntry.EntryNr}&diary={createdEntry.DiaryId}");

    // Assert
    Assert.Equal(HttpStatusCode.OK, responseDelete.StatusCode);
  }
}
