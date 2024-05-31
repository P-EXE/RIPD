using RIPDShared.Models;

namespace RIPDApp.Services;

public interface IDiaryService
{
  Task<bool> AddFoodToDiaryAsync(Food_DiaryEntryDTO_Create foodDiaryEntryDTOCreate);
}
