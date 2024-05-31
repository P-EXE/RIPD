using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class Food_AMProfile : Profile
{
  public Food_AMProfile()
  {
    CreateMap<Food_Create, Food>();
  }
}
