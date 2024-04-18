namespace RIPDApp.Services;

public class UserService : IUserService
{
  private readonly IHttpService _httpService;
  public UserService(IHttpService httpService)
  {
    _httpService = httpService;
  }
}
