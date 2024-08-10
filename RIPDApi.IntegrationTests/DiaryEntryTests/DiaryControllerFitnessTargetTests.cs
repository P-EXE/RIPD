using Amazon.SecurityToken.Model;
using RIPDApi.IntegrationTests.Setup;
using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

[Collection("WithUser")]
public class DiaryControllerFitnessTargetTests(UserFixture userFixture)
{
  private readonly UserFixture _userFixture = userFixture;

  [Fact]
  public async Task GetFitnessTarget_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await _userFixture.Client.GetAsync("api/diary/fitnesstarget");
    DiaryEntry_FitnessTarget? fitnessTarget = await JsonSerializer.DeserializeAsync<DiaryEntry_FitnessTarget>(await response.Content.ReadAsStreamAsync());

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(fitnessTarget);
  }

  [Fact]
  public async Task UpdateFitnessTarget_Valid()
  {
    // Arrange
    DiaryEntry_FitnessTarget_Update update = new()
    {
      EntryNr = 1,
      Acted = DateTime.UtcNow,
      DiaryId = _userFixture.User.Id,
      Weight = 100,
      TargetDateTime = DateTime.UtcNow.AddDays(1)
    };

    // Act
    HttpResponseMessage response = await _userFixture.Client.PutAsJsonAsync("api/diary/fitnesstarget", update, _userFixture.JsonOpt);
    DiaryEntry_FitnessTarget? fitnessTarget = await JsonSerializer.DeserializeAsync<DiaryEntry_FitnessTarget>(await response.Content.ReadAsStreamAsync());

    // Assert
    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    Assert.NotNull(fitnessTarget);
  }
}
