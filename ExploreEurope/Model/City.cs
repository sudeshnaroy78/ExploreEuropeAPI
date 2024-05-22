using System;
using System.ComponentModel.DataAnnotations;

namespace ExploreEurope.Model
{
	public class City
	{
        [Key]
		public int CityId { get; set; }
        public string CityName { get; set; }
        public string CityDescription { get; set; }
        public string CityImage { get; set; }
        public string Transportation { get; set; }

        public int CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<Attraction> Attractions { get; } = new List<Attraction>();
    }
}

