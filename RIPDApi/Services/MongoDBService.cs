using MongoDB.Driver;
using System.Text.Json;
using RIPDApi.Data;

namespace RIPDApi.Services;

public class MongoDBService
{
  private readonly JsonSerializerOptions _jsonSerializerOptions;
  private MongoClient _mongoClient;
  private IMongoDatabase _mongoDatabase;

  public MongoDBService()
  {
    _mongoClient = new MongoClient("mongodb://localhost:27017");
    _mongoDatabase = _mongoClient.GetDatabase("RIPDDB2");

    _jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
  }

  public async Task SaveToMongoDBAsync<T>(string collection, T o)
  {
    IMongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(collection, null);
    mongoCollection.InsertOne(o);
  }

  public async Task<T> GetFromMongoDBAsync<T>(string collection, string id)
  {
    IMongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(collection, null);
    T? ret = await mongoCollection.Find(Builders<T>.Filter.Eq("_id", id)).FirstOrDefaultAsync();
    return ret;
  }

  public async Task DeleteFromMongoDBAsync<T>(string collection, string id)
  {
    IMongoCollection<T> mongoCollection = _mongoDatabase.GetCollection<T>(collection, null);
    await mongoCollection.FindOneAndDeleteAsync(id);
  }
}
