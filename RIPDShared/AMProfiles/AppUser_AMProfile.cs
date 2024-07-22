using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class AppUser_AMProfile : Profile
{
  // Source -> Destination
  public AppUser_AMProfile()
  {
    // Transit -> Rest
    CreateMap<AppUser_Create, AppUser>();
    CreateMap<AppUser_Update, AppUser>();

    // Rest -> Transit
    CreateMap<AppUser, AppUser_Update>();
  }
}
