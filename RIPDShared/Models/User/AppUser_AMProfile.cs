using AutoMapper;

namespace RIPDShared.Models;

public class AppUser_AMProfile : Profile
{
  public AppUser_AMProfile()
  {
    CreateMap<AppUser_DTOCreate, AppUser>();
  }
}
