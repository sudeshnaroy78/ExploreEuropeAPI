using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace ExploreEurope.Model
{
	public class Attraction
    {
        [Key]
        public int AttractionId { get; set; }
        
        public string AttractionName { get; set; }
        public string Description { get; set; }
        public string AttractionImage { get; set; }
        public string Location { get; set; }
        public string AttractionType { get; set; }

        public int CityId { get; set; }
        public City City { get; set; } = null!;
    }
}

