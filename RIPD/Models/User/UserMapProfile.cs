using AutoMapper;

namespace RIPD.Models;

public class UserMapProfile : Profile
{
  public UserMapProfile()
  {
    CreateMap<User, Owner>();
  }
}
