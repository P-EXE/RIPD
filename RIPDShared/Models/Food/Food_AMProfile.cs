using AutoMapper;

namespace RIPDShared.Models;

public class Food_AMProfile : Profile
{
	public Food_AMProfile()
	{
		CreateMap<FoodDTO_Create, Food>();
	}
}
