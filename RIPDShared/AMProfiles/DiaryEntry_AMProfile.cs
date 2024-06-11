using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles
{
  public class DiaryEntry_AMProfile : Profile
  {
    public DiaryEntry_AMProfile()
    {
      // Transit -> Rest
      // Create
      CreateMap<DiaryEntry_Food_Create, DiaryEntry_Food>();
      CreateMap<DiaryEntry_Workout_Create, DiaryEntry_Workout>();
      CreateMap<DiaryEntry_Run_Create, DiaryEntry_Run>();
      // Update
      CreateMap<DiaryEntry_Food_Update, DiaryEntry_Food>();
      CreateMap<DiaryEntry_Workout_Update, DiaryEntry_Workout>();
      CreateMap<DiaryEntry_Run_Update, DiaryEntry_Run>();

      // Rest -> Transit
      // Create
      CreateMap<DiaryEntry_Food, DiaryEntry_Food_Create>();
    }
  }
}
