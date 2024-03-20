using RIPD.Models;
using RIPD.Models.ApiConnection;
using System.Diagnostics;
using System.Text;
using System.Text.Json;

namespace RIPD.DataServices;

/// <summary>
/// <para>
/// DataService responsible for transfer of Users between Client and API.
/// </para>
/// <para>
/// ( Not yet implemented! ) --> Hands off the transfer to UserDataServiceLocal if a connection can't be established.
/// </para>
/// </summary>
/// <remarks>
/// Author: Paul
/// </remarks>
public class UserDataServiceAPI
{
  // API fields
  private readonly HttpClient _httpClient;
  private readonly string _baseAddress;
  private readonly string _url;

  // Serialization fields
  private readonly JsonSerializerOptions _jsonSerializerOptions;

  // Local DB EF Core fields
  private readonly LocalDBContext _localDBContext;

  public UserDataServiceAPI(LocalDBContext localDBContext)
  {
    _httpClient = new HttpClient();

    _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? AndroidApiConnection.BaseAddress : WindowsApiConnection.BaseAddress;
    _url = _baseAddress + "users";

    _jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };

    _localDBContext = localDBContext;
  }

  /// <summary>
  /// Creates a User asynchronously
  /// Via a POST HTTP request to the API
  /// ((or hands it over to the UserDataServiceLocal) not yet implemented)
  /// </summary>
  /// <param name="user">A User</param>
  /// <returns>Nothing for now</returns>
  public async Task CreateAsync(User user)
  {
    User userRes = new();
    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
    {
      Debug.WriteLine("--> Custom(Error): UserDataService.CreateAsync: No Internet Access");
      return;
    }

    string jsonUser = JsonSerializer.Serialize<User>(user, _jsonSerializerOptions);
    StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

    try
    {
      HttpResponseMessage response = await _httpClient.PostAsync($"{_url}", content);
      switch (response.StatusCode)
      {
        case System.Net.HttpStatusCode.Created:
          {
            string res = await response.Content.ReadAsStringAsync();
            userRes = JsonSerializer.Deserialize<User>(res, _jsonSerializerOptions);
            await _localDBContext.AddAsync(userRes);
            _localDBContext.SaveChanges();
            Debug.WriteLine("--> Custom(Success): UserDataService.CreateAsync: Added User");
            break;
          }
        default:
          {
            Debug.WriteLine("--> Custom(Error): UserDataService.CreateAsync: Non 2XX http Response");
            Debug.WriteLine(response);
            break;
          }
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"--> StdEx(Error): UserDataService.CreateAsync: {ex.Message}");
    }
    return;
  }

  /// <summary>
  /// Gets a User asynchronously via Id
  /// Via a GET HTTP request to the API (.../users/{id})
  /// ((or hands it over to the UserDataServiceLocal) not yet implemented)
  /// </summary>
  /// <param name="id">A User.Id</param>
  /// <returns>A singular User</returns>
  public async Task<User> GetOneAsync(int id)
  {
    User? user = new();
    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
    {
      Debug.WriteLine("--> Custom(Error): UserDataService.GetById: No Internet Access");
      return user;
    }

    try
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");
      switch (response.StatusCode)
      {
        case System.Net.HttpStatusCode.OK:
          {
            string content = await response.Content.ReadAsStringAsync();
            user = JsonSerializer.Deserialize<User>(content, _jsonSerializerOptions);
            break;
          }
        default:
          {
            Debug.WriteLine("--> Custom(Error): FoodDataService.GetAllFoodsAsync: Non 2XX http Response");
            break;
          }
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"--> StdEx(Error): FoodDataService.GetAllFoodsAsync: {ex.Message}");
    }

    return user;
  }

  /// <summary>
  /// Gets a User asynchronously via a tuple with a value unique to a User
  /// Via a GET HTTP request to the API (.../users?param=unique)
  /// ((or hands it over to the UserDataServiceLocal) not yet implemented)
  /// </summary>
  /// <param name="unique">Tuple with a Key corresponding to a User parameter and Value unique to a User</param>
  /// <returns>A singular User</returns>
  public async Task<User> GetOneAsync((string, string) unique)
  {
    IEnumerable<User?> users;
    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
    {
      Debug.WriteLine("--> Custom(Error): UserDataService.GetByUnique: No Internet Access");
      return null;
    }

    try
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/?{unique.Item1}={unique.Item2}");
      switch (response.StatusCode)
      {
        case System.Net.HttpStatusCode.OK:
          {
            string content = await response.Content.ReadAsStringAsync();
            users = JsonSerializer.Deserialize<IEnumerable<User>>(content, _jsonSerializerOptions);
            if (users.Count() < 1)
            {
              return null;
            }
            break;
          }
        default:
          {
            Debug.WriteLine("--> Custom(Error): UserDataService.GetByUniqueAsync: Non 2XX http Response");
            return null;
          }
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"--> StdEx(Error): UserDataService.GetByUniqueAsync: {ex.Message}");
      return null;
    }

    return users.First();
  }

  /// <summary>
  /// Get multiple Users asynchronously via a Dictionary with matchable Keys and Values
  /// Via a GET HTTP request to the API (.../users?param1=common param2=common)
  /// ((or hands it over to the UserDataServiceLocal) not yet implemented)
  /// </summary>
  /// <param name="queryParams">Dictionary of User Parameters, common/unique Values</param>
  /// <returns>A List of Users</returns>
  public async Task<List<User>> GetMultipleAsync(Dictionary<string, string> queryParams)
  {
    // Prepare User and Query string
    List<User>? users = new();
    string query = "?";
    foreach (KeyValuePair<string, string> pair in queryParams)
    {
      query += $"{pair.Key}={pair.Value}&";
    }

    // Check if Internet connection is available
    if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
    {
      Debug.WriteLine("--> Custom(Error): UserDataService.GetById: No Internet Access");
      return users;
    }

    // Start REST API query
    try
    {
      HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{query}");

      // Handle Response
      switch (response.StatusCode)
      {
        case System.Net.HttpStatusCode.OK:
          {
            string content = await response.Content.ReadAsStringAsync();
            users = JsonSerializer.Deserialize<List<User>>(content, _jsonSerializerOptions);
            break;
          }
        default:
          {
            Debug.WriteLine("--> Custom(Error): UserDataService.GetMultipleAsync: Non 2XX http Response");
            break;
          }
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"--> StdEx(Error): UserDataService.GetMultipleAsync: {ex.Message}");
    }

    return users;
  }
}
