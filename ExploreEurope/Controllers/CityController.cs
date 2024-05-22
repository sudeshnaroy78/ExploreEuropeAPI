using System;
using AutoMapper;
using ExploreEurope.Data;
using ExploreEurope.DTOs;
using ExploreEurope.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExploreEurope.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : ControllerBase
    {
        private readonly ILogger<CityController> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public CityController(ILogger<CityController> logger, AppDbContext context,IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<City>> GetCity()
        {
            return _context.Cities.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<City>> GetCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);

            if (city == null)
            {
                return NotFound();
            }

            return city;
        }
        [Route("{id}/GetCityByCountryId")]
        [HttpGet]
        public async Task<ActionResult<City>> GetCityByCountryId(int id)
        {
            var city = await _context.Cities.Where(e => e.CountryId == id).ToListAsync();

            if (city == null)
            {
                return NotFound();
            }

            return Ok(city);
        }

        [HttpPost]
        public async Task<ActionResult<City>> PostCity([FromForm]CityDTO cityPayload)
        {
            try {
                string path = Path.GetFullPath(cityPayload.CityImage, "/Users/sudeshnaroy/Downloads/react_app_example/src/wwwroot/City");
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    cityPayload.CityImageFile.CopyTo(stream);
                }
                var city = _mapper.Map<City>(cityPayload);
                _context.Cities.Add(city);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostCity), new { id = city.CityId }, cityPayload);
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest(e.StackTrace);
            }
           
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, CityDTO cityDTO)
        {
            var city = _mapper.Map<City>(cityDTO);
            if (id != city.CityId)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CityExists(id))
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
        public async Task<IActionResult> DeleteCity(int id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(int id)
        {
            return _context.Cities.Any(e => e.CityId == id);
        }
    }
}

