using Azure;
using FluentAssertions;
using RIPDShared.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace RIPDApi.IntegrationTests;

public class FoodControllerTests : IntegrationTest
{
  private readonly Food_Create _validCreateFood;
  public FoodControllerTests()
  {
    _validCreateFood = new()
    {
      Barcode = "1111",
      Name = "Valid Food",
      ManufacturerId = TestUser.Id,
      ContributerId = TestUser.Id,
      Description = "Food description idk",
      Image = "Link here",
      Energy = 0.1f,
      Water = 0.9f,
      Protein = 0.2f,
      Fat = 0.5f,
      Carbohydrates = 12.9f,
      Fiber = 45.2145f,
      Sugar = 1
    };
  }

  [Fact]
  public async void CreateFood_Valid()
  {
    // Arrange

    // Act
    HttpResponseMessage response = await TestHttpClient.PostAsJsonAsync("api/food", _validCreateFood);
    Food? responseFood = JsonSerializer.Deserialize<Food>(await response.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    Assert.NotNull(responseFood);
  }

  [Fact]
  public async void GetFoodById_ValidEmpty()
  {
    // Arrange
    Guid foodId = new("00000000-0000-0000-0000-000000000001");

    // Act
    HttpResponseMessage response = await TestHttpClient.GetAsync($"api/food/{foodId}");

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
  }

  [Fact]
  public async void GetFoodById_ValidFood_AfterFoodCreated()
  {
    // Arrange

    // Act
    HttpResponseMessage createResponse = await TestHttpClient.PostAsJsonAsync("api/food", _validCreateFood);
    createResponse.EnsureSuccessStatusCode();
    Food? createdFood = JsonSerializer.Deserialize<Food>(await createResponse.Content.ReadAsStringAsync(), JsonOptions);
    Assert.NotNull(createdFood);

    HttpResponseMessage readResponse = await TestHttpClient.GetAsync($"api/food/{createdFood.Id}");
    Food? readFood = JsonSerializer.Deserialize<Food>(await readResponse.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.OK, readResponse.StatusCode);
    Assert.NotNull(readFood);
  }

  [Fact]
  public async void GetFoodsByNameAtPosition_ValidEmpty()
  {
    // Arrange
    string name = "name";
    int position = 0;

    // Act
    HttpResponseMessage response = await TestHttpClient.GetAsync($"api/food?name={name}&position={position}");
    IEnumerable<Food>? readFoods = JsonSerializer.Deserialize<IEnumerable<Food>>(await response.Content.ReadAsStringAsync(), JsonOptions);

    // Assert
    Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    Assert.Equal([], readFoods);
  }
}
