using AutoMapper;
using RIPDShared.Models;
using System.Net;

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

  public async Task<bool> AddFoodEntryyAsync(DiaryEntry_Food entry)
  {
    // Mapping
    DiaryEntry_Food_Create createEntry = _mapper.Map<DiaryEntry_Food_Create>(entry);

    // Api
    return await _httpService.PostAsync("diary/food", createEntry);

    // Return
  }

  public async Task<bool> AddWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    // Mapping
    DiaryEntry_Workout_Create create = _mapper.Map<DiaryEntry_Workout_Create>(entry);

    // Api
    return await _httpService.PostAsync("diary/workout", create);

    // Return
  }

  public async Task<DiaryEntry_BodyMetric?> AddBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    // Mapping
    DiaryEntry_BodyMetric_Create create = _mapper.Map<DiaryEntry_BodyMetric_Create>(entry);
    create.DiaryId = Statics.Auth.Owner.Id;

    // Api
    return await _httpService.PostAsync<DiaryEntry_BodyMetric_Create, DiaryEntry_BodyMetric>("diary/bodymetric", create);

    // Return
  }

  public async Task<IEnumerable<DiaryEntry_BodyMetric>?> GetBodyMetricEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    Dictionary<string, object> queries = new()
    {
      ["diary"] = diary.OwnerId,
      ["startDate"] = startDate,
      ["endDate"] = endDate,
    };
    return await _httpService.GetAsync<IEnumerable<DiaryEntry_BodyMetric>?>($"diary/bodymetric", queries);
  }

  public async Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    // Mapping
    DiaryEntry_BodyMetric_Update update = _mapper.Map<DiaryEntry_BodyMetric_Update>(entry);

    // Api
    return await _httpService.PutAsync<DiaryEntry_BodyMetric_Update, DiaryEntry_BodyMetric>("diary/bodymetric", update);

    // Return
  }

  public async Task<bool> DeleteBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    Dictionary<string, object> queries = new()
    {
      ["entry"] = entry.EntryNr,
      ["diary"] = entry.DiaryId,
    };

    // Api
    return await _httpService.DeleteAsync<bool>("diary/bodymetric", queries);
  }

  public async Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    Dictionary<string, object> queries = new()
    {
      ["diary"] = diary.OwnerId,
      ["startDate"] = startDate,
      ["endDate"] = endDate,
    };
    return await _httpService.GetAsync<IEnumerable<DiaryEntry_Food>?>($"diary/foods", queries);
  }
}
