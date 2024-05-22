using System;
namespace ExploreEurope.DTOs
{
	public class CityDTO
	{
        public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public string CityImage { get; set; }
        public IFormFile? CityImageFile { get; set; }
        
        public string Transportation { get; set; }

        public int CountryId { get; set; }
    }
}

