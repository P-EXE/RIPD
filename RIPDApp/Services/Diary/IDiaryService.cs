using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IDiaryService
{
  Task<bool> AddFoodToDiaryAsync(DiaryEntry_Food_Create foodDiaryEntryDTOCreate);
}
