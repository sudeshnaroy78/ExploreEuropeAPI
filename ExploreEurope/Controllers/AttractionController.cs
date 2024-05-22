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
    //First Commit
    [ApiController]
    [Route("[controller]")]
    public class AttractionController : ControllerBase
    {
        private readonly ILogger<AttractionController> _logger;
        private IMapper _mapper;

        private readonly AppDbContext _context;
        public AttractionController(ILogger<AttractionController> logger, AppDbContext context,IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;

        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractions()
        {
            return await _context.Attractions.ToListAsync();

            //If we want to fetch referrencial data , all country with associated city list
            //_context.Countries.Include(e=>e.Cities).ToList()
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Attraction>> GetAttraction(int id)
        {
            var Attraction = await _context.Attractions.FindAsync(id);

            if (Attraction == null)
            {
                return NotFound();
            }

            return Attraction;
        }
        [Route("{id}/GetAttractionByCityId")]
        [HttpGet]
        public async Task<ActionResult<Attraction>> GetAttractionByCityId(int id)
        {
            var attractions = await _context.Attractions.Where(e => e.CityId == id).ToListAsync();

            if (attractions == null)
            {
                return NotFound();
            }

            return Ok(attractions);
        }

        [HttpPost]
        public async Task<ActionResult<Attraction>> PostCountry([FromForm]AttractionDTO attractionDto)
        {
            try {
                //Directory.GetCurrentDirectory(),
                string path = Path.GetFullPath(attractionDto.AttractionImage, "/Users/sudeshnaroy/Downloads/react_app_example/src/wwwroot/Attraction");
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    attractionDto.attractionImageFile.CopyTo(stream);
                }
                var attraction = _mapper.Map<Attraction>(attractionDto);
                _context.Attractions.Add(attraction);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(PostCountry), new { id = attraction.AttractionId }, attractionDto);
            }
            catch(Exception e)
            {
                _logger.LogError(e.StackTrace);
                return BadRequest();
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, AttractionDTO attractionDTO)
        {
            var attraction = _mapper.Map<Attraction>(attractionDTO);
            if (id != attraction.AttractionId)
            {
                return BadRequest();
            }

            _context.Entry(attraction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AttractionExists(id))
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
        public async Task<IActionResult> DeleteAttraction(int id)
        {
            var attraction = await _context.Attractions.FindAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            _context.Attractions.Remove(attraction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AttractionExists(int id)
        {
            return _context.Attractions.Any(e => e.AttractionId == id);
        }

    }
}


