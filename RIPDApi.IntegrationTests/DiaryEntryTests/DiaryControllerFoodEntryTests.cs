using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class DiaryControllerFoodEntryTests
{
  private readonly UserFixture _fixture;

  public DiaryControllerFoodEntryTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task EntryFood_WithFood_Create_Valid()
  {
    // Arrange
    Food_Create createFood = new()
    {
      Barcode = "1",
      Name = "Food Entry test Food Name",
      ManufacturerId = _fixture.TestUser.Id,
      ContributerId = _fixture.TestUser.Id,
      Description = "Food Entry test Food Description",
      Image = "Test Image"
    };

    HttpResponseMessage responseCreatedFood = await _fixture.TestClient.PostAsJsonAsync("api/food", createFood, _fixture.JsonOpt);
    Food? createdFood = JsonSerializer.Deserialize<Food>(await responseCreatedFood.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedFood.StatusCode);
    Assert.NotNull(createdFood);

    DiaryEntry_Food_Create createEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      FoodId = createdFood.Id,
      Amount = 100
    };

    // Act
    HttpResponseMessage responseCreateEntry = await _fixture.TestClient.PostAsJsonAsync("api/diary/food", createEntry, _fixture.JsonOpt);
    DiaryEntry_Food? createdEntry = JsonSerializer.Deserialize<DiaryEntry_Food>(await responseCreateEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, responseCreateEntry.StatusCode);
    Assert.NotNull(createdEntry);
  }

  [Fact]
  public async Task EntryFood_Get_FromToDate_ValidEmpty()
  {
    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/food");
    IEnumerable<DiaryEntry_Food>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_Food>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
  }

  [Fact]
  public async Task EntryFood_Get_FromToDate_Valid()
  {
    // Arrange
    await EntryFood_WithFood_Create_Valid();
    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/food");
    IEnumerable<DiaryEntry_Food>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_Food>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
    Assert.True(got.Any());
  }

  [Fact]
  public async Task EntryFood_CreateAndUpdate_Valid()
  {
    // Arrange
    Food_Create createFood = new()
    {
      Barcode = "1",
      Name = "Food Entry test Food Name",
      ManufacturerId = _fixture.TestUser.Id,
      ContributerId = _fixture.TestUser.Id,
      Description = "Food Entry test Food Description",
      Image = "Test Image"
    };

    HttpResponseMessage responseCreatedFood = await _fixture.TestClient.PostAsJsonAsync("api/food", createFood, _fixture.JsonOpt);
    Food? createdFood = JsonSerializer.Deserialize<Food>(await responseCreatedFood.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreatedFood.StatusCode);
    Assert.NotNull(createdFood);

    DiaryEntry_Food_Create createEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      FoodId = createdFood.Id,
      Amount = 100
    };

    HttpResponseMessage responseCreateEntry = await _fixture.TestClient.PostAsJsonAsync("api/diary/food", createEntry, _fixture.JsonOpt);
    DiaryEntry_Food? createdEntry = JsonSerializer.Deserialize<DiaryEntry_Food>(await responseCreateEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreateEntry.StatusCode);
    Assert.NotNull(createdEntry);

    DiaryEntry_Food_Update updateEntry = new()
    {
      DiaryId = _fixture.TestUser.Id,
      EntryNr = createdEntry.EntryNr,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      FoodId = createdFood.Id ?? new(),
      Amount = 101
    };

    // Act
    HttpResponseMessage responseUpdatedEntry = await _fixture.TestClient.PutAsJsonAsync("api/diary/food", updateEntry, _fixture.JsonOpt);
    DiaryEntry_Food? updatedEntry = JsonSerializer.Deserialize<DiaryEntry_Food>(await responseUpdatedEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, responseUpdatedEntry.StatusCode);
    Assert.NotNull(updatedEntry);
    Assert.Equal(createdEntry.EntryNr, updatedEntry.EntryNr);
    Assert.NotEqual(createdEntry.Amount, updatedEntry.Amount);
  }
}
