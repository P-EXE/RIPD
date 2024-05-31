using RIPDShared.Models;

namespace RIPDApp.Services;

public class DiaryService : IDiaryService
{
  private readonly IHttpService _httpService;
  public DiaryService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<bool> AddFoodToDiaryAsync(DiaryEntry_Food_Create foodDiaryEntryDTOCreate)
  {
    return await _httpService.PostAsync("diary/foods", foodDiaryEntryDTOCreate);
  }
}
