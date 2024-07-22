using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class DiaryEntry_BodyMetric_AMProfile : Profile
{
  // Source -> Destination
  public DiaryEntry_BodyMetric_AMProfile()
  {
    // Transit -> Rest
    CreateMap<DiaryEntry_BodyMetric_Create, DiaryEntry_BodyMetric>();
    CreateMap<DiaryEntry_BodyMetric_Update, DiaryEntry_BodyMetric>();
    // Rest -> Transit
    CreateMap<DiaryEntry_BodyMetric, DiaryEntry_BodyMetric_Create>();
    CreateMap<DiaryEntry_BodyMetric, DiaryEntry_BodyMetric_Update>();
    // No Read DTO Intended as of now.
  }
}
