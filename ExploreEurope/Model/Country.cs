using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace ExploreEurope.Model
{
	public class Country
	{

        [Key]
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public string CountryImage { get; set; }

        public ICollection<City> Cities { get; } = new List<City>();
    }
}


