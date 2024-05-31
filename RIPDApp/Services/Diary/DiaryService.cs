using RIPDShared.Models;

namespace RIPDApp.Services;

public class DiaryService : IDiaryService
{
  private readonly IHttpService _httpService;
  public DiaryService(IHttpService httpService)
  {
    _httpService = httpService;
  }

  public async Task<bool> AddFoodToDiaryAsync(Food_DiaryEntryDTO_Create foodDiaryEntryDTOCreate)
  {
    return await _httpService.PostAsync("diary/foods", foodDiaryEntryDTOCreate);
  }
}
