using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

class DiaryEntry_Run_AMProfile : Profile
{
  public DiaryEntry_Run_AMProfile()
  {
    // Source -> Destination
    CreateMap<DiaryEntry_Run_Create, DiaryEntry_Run>();
    CreateMap<DiaryEntry_Run_Update, DiaryEntry_Run>();

    CreateMap<DiaryEntry_Run, DiaryEntry_Run_Create>();
    CreateMap<DiaryEntry_Run, DiaryEntry_Run_Create>();
  }
}
