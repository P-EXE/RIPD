using AutoMapper;
using RIPDShared.Models;

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

  public async Task<DiaryEntry_Food?> AddFoodEntryAsync(DiaryEntry_Food entry)
  {
    // Mapping
    DiaryEntry_Food_Create createEntry = _mapper.Map<DiaryEntry_Food_Create>(entry);

    // Api
    return await _httpService.PostAsync<DiaryEntry_Food_Create, DiaryEntry_Food>("diary/food", createEntry);

    // Return
  }

  public async Task<DiaryEntry_Workout?> AddWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    // Mapping
    DiaryEntry_Workout_Create create = _mapper.Map<DiaryEntry_Workout_Create>(entry);

    // Api
    return await _httpService.PostAsync<DiaryEntry_Workout_Create, DiaryEntry_Workout>("diary/workout", create);

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

  public async Task<DiaryEntry_Run?> AddRunEntryAsync(DiaryEntry_Run entry)
  {
    // Mapping
    DiaryEntry_Run_Create create = _mapper.Map<DiaryEntry_Run_Create>(entry);

    return await _httpService.PostAsync<DiaryEntry_Run_Create,DiaryEntry_Run>("diary/run", create);
  }


  public async Task<IEnumerable<DiaryEntry_Food>?> GetFoodEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    Dictionary<string, object> queries = new()
    {
      ["diary"] = diary.OwnerId,
      ["startDate"] = startDate.ToString("yyyy-MM-ddThh:mm:ss.FFFZ"),
      ["endDate"] = endDate.ToString("yyyy-MM-ddThh:mm:ss.FFFZ"),
    };
    return await _httpService.GetAsync<IEnumerable<DiaryEntry_Food>?>($"diary/food", queries);
  }

  public async Task<IEnumerable<DiaryEntry_Workout>?> GetWorkoutEntriesAsync(Diary diary, DateTime startDate, DateTime endDate)
  {
    Dictionary<string, object> queries = new()
    {
      ["diary"] = diary.OwnerId,
      ["startDate"] = startDate.ToString("yyyy-MM-ddThh:mm:ss.FFFZ"),
      ["endDate"] = endDate.ToString("yyyy-MM-ddThh:mm:ss.FFFZ"),
    };
    return await _httpService.GetAsync<IEnumerable<DiaryEntry_Workout>?>($"diary/workout", queries);
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

  public Task<IEnumerable<DiaryEntry_Run>?> GetRunEntriesAsync(DateTime startDate, DateTime endDate)
  {
    throw new NotImplementedException();
  }

  public async Task<DiaryEntry_FitnessTarget?> GetFitnessTargetEntryAsync()
  {
    return await _httpService.GetAsync<DiaryEntry_FitnessTarget?>($"diary/fitnesstarget");
  }


  public Task<DiaryEntry_Food?> UpdateFoodEntryAsync(DiaryEntry_Food entry)
  {
    throw new NotImplementedException();
  }

  public Task<DiaryEntry_Workout?> UpdateWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    throw new NotImplementedException();
  }

  public async Task<DiaryEntry_BodyMetric?> UpdateBodyMetricEntryAsync(DiaryEntry_BodyMetric entry)
  {
    // Mapping
    DiaryEntry_BodyMetric_Update update = _mapper.Map<DiaryEntry_BodyMetric_Update>(entry);

    // Api
    return await _httpService.PutAsync<DiaryEntry_BodyMetric_Update, DiaryEntry_BodyMetric>("diary/bodymetric", update);

    // Return
  }

  public async Task<DiaryEntry_FitnessTarget?> UpdateFitnessTargetEntryAsync(DiaryEntry_FitnessTarget entry)
  {
    DiaryEntry_FitnessTarget_Update update = _mapper.Map<DiaryEntry_FitnessTarget_Update>(entry);
    return await _httpService.PutAsync<DiaryEntry_FitnessTarget_Update, DiaryEntry_FitnessTarget?>($"diary/fitnesstarget", update);
  }


  public async Task<bool> DeleteFoodEntryAsync(DiaryEntry_Food entry)
  {
    Dictionary<string, object> queries = new()
    {
      ["entry"] = entry.EntryNr,
      ["diary"] = entry.DiaryId,
    };

    // Api
    return await _httpService.DeleteAsync<bool>("diary/food", queries);
  }

  public async Task<bool> DeleteWorkoutEntryAsync(DiaryEntry_Workout entry)
  {
    Dictionary<string, object> queries = new()
    {
      ["entry"] = entry.EntryNr,
      ["diary"] = entry.DiaryId,
    };

    // Api
    return await _httpService.DeleteAsync<bool>("diary/workout", queries);
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

  public Task<bool> DeleteRunEntryAsync(DiaryEntry_Run entry)
  {
    throw new NotImplementedException();
  }
}
