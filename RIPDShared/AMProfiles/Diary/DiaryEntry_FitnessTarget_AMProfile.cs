using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class DiaryEntry_FitnessTarget_AMProfile : Profile
{
  public DiaryEntry_FitnessTarget_AMProfile()
  {
    // Transit -> Rest
    CreateMap<DiaryEntry_FitnessTarget_Update, DiaryEntry_FitnessTarget>();
    CreateMap<DiaryEntry_FitnessTarget, DiaryEntry_FitnessTarget_Update>();
  }
}
