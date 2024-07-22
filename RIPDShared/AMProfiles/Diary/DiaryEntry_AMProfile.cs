using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class DiaryEntry_AMProfile : Profile
{

  // Source -> Target
  public DiaryEntry_AMProfile()
  {
    // Transit -> Rest
    CreateMap<DiaryEntry_Create, DiaryEntry>()
      .ForAllMembers(m => m.AllowNull());

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
  }
}
