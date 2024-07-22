using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class DiaryEntry_Workout_AMProfile : Profile
{
  // Source -> Destination
  public DiaryEntry_Workout_AMProfile()
  {
    // Transit -> Rest
    CreateMap<DiaryEntry_Workout_Create, DiaryEntry_Workout>();
    CreateMap<DiaryEntry_Workout_Update, DiaryEntry_Workout>();
    // Rest -> Transit
    CreateMap<DiaryEntry_Workout, DiaryEntry_Workout_Create>();
    CreateMap<DiaryEntry_Workout, DiaryEntry_Workout_Update>();
    // No Read DTO Intended as of now.
  }
}
