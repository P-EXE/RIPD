using RIPD.DataBase;
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

  public UserDataServiceAPI()
  {
    _httpClient = new HttpClient();

    _baseAddress = DeviceInfo.Platform == DevicePlatform.Android ? AndroidApiConnection.BaseAddress : WindowsApiConnection.BaseAddress;
    _url = _baseAddress + "users";

    _jsonSerializerOptions = new JsonSerializerOptions
    {
      PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
    };
  }

  public async Task<User> CreateUserAsync(User_CreateDTO user)
  {
    try
    {
      string jsonUser = JsonSerializer.Serialize(user, _jsonSerializerOptions);
      StringContent content = new StringContent(jsonUser, Encoding.UTF8, "application/json");

      HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/register", content);

      if (response.IsSuccessStatusCode)
      {
        Debug.WriteLine("==Success==> UserDataServiceApi / CreateUser : Created User @ API");
        string responseContent = await response.Content.ReadAsStringAsync();
        User responseUser = JsonSerializer.Deserialize<User>(responseContent, _jsonSerializerOptions);
        return responseUser;
      }
      else
      {
        Debug.WriteLine("==Error==> UserDataServiceApi / CreateUser : Couldn't create User @ API");
        return null;
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"==Exception==> UserDataServiceApi / CreateUser : {ex}");
      return null;
    }
  }
  public async Task<User> GetUserByEmailAndPassword(string email, string password)
  {
    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>()
    {
      ["email"] = email,
      ["password"] = password
    };

    try
    {
      string info = JsonSerializer.Serialize(keyValuePairs, _jsonSerializerOptions);
      StringContent content = new StringContent(info, Encoding.UTF8, "application/json");
      HttpResponseMessage response = await _httpClient.PostAsync($"{_url}/login", content);

      if (response.IsSuccessStatusCode)
      {
        Debug.WriteLine("==Success==> UserDataServiceApi / GetUserByEmailAndPassword : Got User");
        string responseContent = await response.Content.ReadAsStringAsync();
        User responseUser = JsonSerializer.Deserialize<User>(responseContent, _jsonSerializerOptions);
        return responseUser;
      }
      else
      {
        Debug.WriteLine("==Error==> UserDataServiceApi / GetUserByEmailAndPassword : Couldn't get User");
        return null;
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"==Error==> UserDataServiceApi / GetUserByEmailAndPassword : {ex}");
      return null;
    }
  }
  public async Task DeleteUserAsync(int id)
  {
    try
    {
      HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/{id}");

      if (response.IsSuccessStatusCode)
      {
        Debug.WriteLine("==Success==> UserDataServiceApi / DeleteUser : Deleted User @ API");
      }
      else
      {
        Debug.WriteLine("==Error==> UserDataServiceApi / DeleteUser : Couldn't delete User @ API");
      }
    }
    catch (Exception ex)
    {
      Debug.WriteLine($"==Exception==> UserDataServiceApi / DeleteUser : {ex}");
    }
  }
}
