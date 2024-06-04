using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class Food_AMProfile : Profile
{
  // Source -> Destination
  public Food_AMProfile()
  {
    // Transit -> Rest
    CreateMap<Food_Create, Food>();
    CreateMap<Food_Update, Food>();
    // Rest -> Transit
    CreateMap<Food, Food_Create>()
      .ForSourceMember(s => s.Manufacturer, o => o.DoNotValidate())
      .ForSourceMember(s => s.Contributer, o => o.DoNotValidate())
      .ForAllMembers(o => o.AllowNull());
    CreateMap<Food, Food_Update>();
    // No Read DTO Intended as of now.
  }
}
