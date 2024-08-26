using RIPDApi.IntegrationTests.Setup;
using RIPDShared.Models;
using System.Net.Http.Json;
using System.Net;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class DiaryControllerRunEntryTests
{
  private readonly UserFixture _fixture;

  public DiaryControllerRunEntryTests(UserFixture fixture)
  {
    _fixture = fixture;
  }

  [Fact]
  public async Task EntryRun_Create_Valid()
  {
    // Arrange
    DiaryEntry_Run_Create createEntry = new()
    {
      DiaryId = _fixture.User.Id,
      Acted = DateTime.UtcNow,
      Added = DateTime.UtcNow,
      Locations = [new() { Latitude = 10, Longitude = 10, Timestamp = DateTime.UtcNow}]
    };

    // Act
    HttpResponseMessage responseCreateEntry = await _fixture.Client.PostAsJsonAsync("api/diary/run", createEntry, _fixture.JsonOpt);
    var createdEntry = JsonSerializer.Deserialize<DiaryEntry_Run>(await responseCreateEntry.Content.ReadAsStringAsync(), _fixture.JsonOpt);

    // Assert
    Assert.Equal(HttpStatusCode.Created, responseCreateEntry.StatusCode);
    Assert.NotNull(createdEntry);
    Assert.Equal(10, createdEntry.Run.Locations.First().Latitude);
    Assert.Equal(10, createdEntry.Run.Locations.First().Longitude);
  }
}