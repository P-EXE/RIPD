using AutoMapper;
using RIPDShared.Models;

namespace RIPDShared.AMProfiles;

public class Workout_AMProfile : Profile
{
  // Source -> Destination
  public Workout_AMProfile()
  {
    // Transit -> Rest
    CreateMap<Workout_Create, Workout>();
    CreateMap<Workout_Update, Workout>();
    // Rest -> Transit
    CreateMap<Workout, Workout_Create>()
      .ForSourceMember(s => s.Contributer, o => o.DoNotValidate())
      .ForAllMembers(o => o.AllowNull());
    CreateMap<Workout, Workout_Update>();
    // No Read DTO Intended as of now.
  }
}
