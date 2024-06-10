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

  public async Task<bool> AddFoodToDiaryAsync(DiaryEntry_Food entry)
  {
    // Mapping
    DiaryEntry_Food_Create createEntry = _mapper.Map<DiaryEntry_Food_Create>(entry);

    // Api
    return await _httpService.PostAsync("diary/foods", createEntry);

    // Return
  }
}
