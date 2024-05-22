using System;
using AutoMapper;
using ExploreEurope.DTOs;
using ExploreEurope.Model;

namespace ExploreEurope
{
	public class MappingProfiles:Profile
	{
		public MappingProfiles()
		{
			CreateMap<CountryDTO, Country>();
			CreateMap<CityDTO, City>();
            CreateMap<AttractionDTO, Attraction>();
        }
	}
}

