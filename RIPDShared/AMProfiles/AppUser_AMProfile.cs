using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class AppUser_AMProfile : Profile
{
  public AppUser_AMProfile()
  {
    CreateMap<AppUserDTO_Create, AppUser>();
  }
}
