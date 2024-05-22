using System;
using AutoMapper;
using System.Net;
using ExploreEurope.Data;
using ExploreEurope.DTOs;
using ExploreEurope.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExploreEurope.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ILogger<CountryController> _logger;
        private IMapper _mapper;

        private readonly AppDbContext _context;
        public CountryController(ILogger<CountryController> logger, AppDbContext context,IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetCountry()
        {
            return _context.Countries.ToList();
            //If we want to fetch referrencial data , all country with associated city list
            //_context.Countries.Include(e=>e.Cities).ToList()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Country>> GetCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            return country;
        }
       

        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry([FromForm]CountryDTO countryDto)
        {
            try {
                //Directory.GetCurrentDirectory(),
                string path = Path.GetFullPath(countryDto.CountryImage, "/Users/sudeshnaroy/Downloads/react_app_example/src/wwwroot");
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    countryDto.CountryImageFile.CopyTo(stream);
                }
                var country = _mapper.Map<Country>(countryDto);
                _context.Countries.Add(country);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostCountry), new { id = country.CountryId }, countryDto);
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest();
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, Country Country)
        {
            if (id != Country.CountryId)
            {
                return BadRequest();
            }

            _context.Entry(Country).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            var article = await _context.Countries.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Countries.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.CountryId == id);
        }

    }
}


