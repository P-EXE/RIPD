using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class DiaryControllerBodyMetricTests
{
  private readonly UserFixture _fixture;

  public DiaryControllerBodyMetricTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  #region BodyMetric Entry
  [Fact]
  public async Task EntryBodyMetric_Create_Valid()
  {
    // Arrange
    DiaryEntry_BodyMetric_Create create = new()
    {
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      DiaryId = _fixture.TestUser.Id,
      Height = 100,
      Weight = 100
    };

    // Act
    HttpResponseMessage response = await _fixture.TestClient.PostAsJsonAsync("api/diary/bodymetric", create, _fixture.JsonOpt);
    DiaryEntry_BodyMetric? created = JsonSerializer.Deserialize<DiaryEntry_BodyMetric>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(created);
  }

  [Fact]
  public async Task EntryBodyMetric_Get_FromToDate_ValidEmpty()
  {
    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/bodymetric");
    IEnumerable<DiaryEntry_BodyMetric>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_BodyMetric>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
  }

  [Fact]
  public async Task EntryBodyMetric_Get_FromToDate_Valid()
  {
    // Arrange
    await EntryBodyMetric_Create_Valid();
    // Act
    HttpResponseMessage response = await _fixture.TestClient.GetAsync("api/diary/bodymetric");
    IEnumerable<DiaryEntry_BodyMetric>? got = JsonSerializer.Deserialize<IEnumerable<DiaryEntry_BodyMetric>>(await response.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.NotNull(got);
    Assert.True(got.Any());
  }

  [Fact]
  public async Task EntryBodyMetric_CreateAndUpdate_Valid()
  {
    // Arrange
    DiaryEntry_BodyMetric_Create create = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      Height = 100,
      Weight = 100
    };

    HttpResponseMessage responseCreated = await _fixture.TestClient.PostAsJsonAsync("api/diary/bodymetric", create, _fixture.JsonOpt);
    DiaryEntry_BodyMetric? created = JsonSerializer.Deserialize<DiaryEntry_BodyMetric>(await responseCreated.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreated.StatusCode);
    Assert.NotNull(created);

    DiaryEntry_BodyMetric_Update update = new()
    {
      DiaryId = _fixture.TestUser.Id,
      EntryNr = created.EntryNr,
      Acted = DateTime.UtcNow,
      Height = 101,
      Weight = 101
    };

    // Act
    HttpResponseMessage responseUpdated = await _fixture.TestClient.PutAsJsonAsync("api/diary/bodymetric", update, _fixture.JsonOpt);
    DiaryEntry_BodyMetric? updated = JsonSerializer.Deserialize<DiaryEntry_BodyMetric>(await responseUpdated.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.OK, responseUpdated.StatusCode);
    Assert.NotNull(updated);
    Assert.Equal(created.EntryNr, updated.EntryNr);
    Assert.NotEqual(created.Height, updated.Height);
    Assert.NotEqual(created.Weight, updated.Weight);
  }

  [Fact]
  public async Task EntryBodyMetric_CreateAndDelete_Valid()
  {
    // Arrange
    DiaryEntry_BodyMetric_Create create = new()
    {
      DiaryId = _fixture.TestUser.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      Height = 100,
      Weight = 100
    };

    HttpResponseMessage responseCreated = await _fixture.TestClient.PostAsJsonAsync("api/diary/bodymetric", create, _fixture.JsonOpt);
    DiaryEntry_BodyMetric? created = JsonSerializer.Deserialize<DiaryEntry_BodyMetric>(await responseCreated.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    Assert.Equal(HttpStatusCode.Created, responseCreated.StatusCode);
    Assert.NotNull(created);

    // Act
    HttpResponseMessage responseDeleted = await _fixture.TestClient.DeleteAsync($"api/diary/bodymetric?entry={created.EntryNr}&diary={created.DiaryId}");

    // Assert
    Assert.Equal(HttpStatusCode.OK, responseDeleted.StatusCode);
  }
  #endregion BodyMetric Entry
}
