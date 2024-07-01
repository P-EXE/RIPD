using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles
{
  public class DiaryEntry_AMProfile : Profile
  {

    // Source -> Target
    public DiaryEntry_AMProfile()
    {
      // Transit -> Rest
      CreateMap<DiaryEntry_Create, DiaryEntry>()
        .ForAllMembers(m => m.AllowNull()); ;
      // Create
      CreateMap<DiaryEntry_Food_Create, DiaryEntry_Food>();
      CreateMap<DiaryEntry_Workout_Create, DiaryEntry_Workout>();

      // Rest -> Transit
      CreateMap<DiaryEntry, DiaryEntry_Create>()
        .ForMember(d => d.DiaryId, s => s.MapFrom(s => s.DiaryId))
        .ForMember(d => d.Acted, s => s.MapFrom(s => s.Acted))
        .ForMember(d => d.Added, s => s.MapFrom(s => s.Added))
        .ForAllMembers(opts => opts.Condition(rc =>
        {
          try { return rc != null; } // Or anything, just try to get the value. 
          catch { return false; }
        }));
      // Create
      CreateMap<DiaryEntry_Food, DiaryEntry_Food_Create>()
        .IncludeBase<DiaryEntry, DiaryEntry_Create>()
        .ForMember(d => d.FoodId, s => s.MapFrom(s => s.FoodId))
        .ForMember(d => d.Amount, s => s.MapFrom(s => s.Amount))
        .ForAllMembers(opts => opts.Condition(rc =>
        {
          try { return rc != null; } // Or anything, just try to get the value. 
          catch { return false; }
        }));
      CreateMap<DiaryEntry_Workout, DiaryEntry_Workout_Create>()
        .IncludeBase<DiaryEntry, DiaryEntry_Create>()
        .ForMember(d => d.WorkoutId, s => s.MapFrom(s => s.WorkoutId))
        .ForMember(d => d.Amount, s => s.MapFrom(s => s.Amount))
        .ForAllMembers(opts => opts.Condition(rc =>
        {
          try { return rc != null; } // Or anything, just try to get the value. 
          catch { return false; }
        }));
    }
  }
}
