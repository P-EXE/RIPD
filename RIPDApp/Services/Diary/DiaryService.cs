using AutoMapper;
using RIPDShared.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RIPDApp.Services;

public class DiaryService : IDiaryService
{
  private readonly IHttpService _httpService;
  private readonly IMapper _mapper;
  public DiaryService(IHttpService httpService, IMapper mapper)
  {
    _httpService = httpService;
    _mapper = mapper;
  }

  public async Task<bool> AddFoodEntryToDiaryAsync(DiaryEntry_Food entry)
  {
    // Mapping
    DiaryEntry_Food_Create createEntry = _mapper.Map<DiaryEntry_Food_Create>(entry);

    // Api
    return await _httpService.PostAsync("diary/foods", createEntry);

    // Return
  }

  public async Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesInDateRange(Diary diary, DateTime startDate, DateTime endDate)
  {
    Dictionary<string, string> queries = new()
    {
      ["startDate"] = startDate.ToString(),
      ["endDate"] = endDate.ToString(),
    };
    return await _httpService.GetAsync<IEnumerable<DiaryEntry_Food>?>($"diary/{diary.OwnerId}/foods", queries);
  }
}
