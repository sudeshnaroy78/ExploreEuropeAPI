using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace ExploreEurope.DTOs
{
	public class CountryDTO
	{

       
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public IFormFile CountryImageFile { get; set; }
        public string CountryImage { get; set; }

        //public ICollection<City> Cities { get; } = new List<City>();
    }
}


