using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class AppUser_AMProfile : Profile
{
  public AppUser_AMProfile()
  {
    // Transit -> Rest
    CreateMap<AppUser_Create, AppUser>();
    CreateMap<AppUser_Update, AppUser>();
  }
}
