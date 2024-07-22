using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class DiaryEntry_Food_AMProfile : Profile
{
  // Source -> Destination
  public DiaryEntry_Food_AMProfile()
  {
    // Transit -> Rest
    CreateMap<DiaryEntry_Food_Create, DiaryEntry_Food>();
    CreateMap<DiaryEntry_Food_Update, DiaryEntry_Food>();
    // Rest -> Transit
    CreateMap<DiaryEntry_Food, DiaryEntry_Food_Create>();
    CreateMap<DiaryEntry_Food, DiaryEntry_Food_Update>();
    // No Read DTO Intended as of now.
  }
}
