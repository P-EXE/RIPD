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
    // No Read DTO Intended as of now.
  }
}
