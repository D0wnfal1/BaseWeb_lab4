using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseWeb_lab4.Models;

namespace BaseWeb_lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApartmentsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApartmentsApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Apartment>>> GetApartments(
            string? district = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            int? minRooms = null,
            int? maxRooms = null)
        {
            var apartments = _context.Apartments.Include(a => a.Owner).AsQueryable();

            if (!string.IsNullOrEmpty(district))
            {
                apartments = apartments.Where(a => a.District.Contains(district));
            }

            if (minPrice.HasValue)
            {
                apartments = apartments.Where(a => a.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                apartments = apartments.Where(a => a.Price <= maxPrice.Value);
            }

            if (minRooms.HasValue)
            {
                apartments = apartments.Where(a => a.Rooms >= minRooms.Value);
            }

            if (maxRooms.HasValue)
            {
                apartments = apartments.Where(a => a.Rooms <= maxRooms.Value);
            }

            return await apartments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Apartment>> GetApartment(int id)
        {
            var apartment = await _context.Apartments
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (apartment == null)
            {
                return NotFound();
            }

            return apartment;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutApartment(int id, Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return BadRequest();
            }

            _context.Entry(apartment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ApartmentExists(id))
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

        [HttpPost]
        public async Task<ActionResult<Apartment>> PostApartment(Apartment apartment)
        {
            _context.Apartments.Add(apartment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetApartment", new { id = apartment.Id }, apartment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApartment(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }

            _context.Apartments.Remove(apartment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}