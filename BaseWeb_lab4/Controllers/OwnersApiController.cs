using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseWeb_lab4.Models;

namespace BaseWeb_lab4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnersApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OwnersApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners(string? name = null, string? email = null)
        {
            var owners = _context.Owners.Include(o => o.Apartments).AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                owners = owners.Where(o => o.Name.Contains(name));
            }

            if (!string.IsNullOrEmpty(email))
            {
                owners = owners.Where(o => o.Email.Contains(email));
            }

            return await owners.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Owner>> GetOwner(int id)
        {
            var owner = await _context.Owners
                .Include(o => o.Apartments)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOwner(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(id))
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
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOwner(int id)
        {
            var owner = await _context.Owners.FindAsync(id);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnerExists(int id)
        {
            return _context.Owners.Any(e => e.Id == id);
        }
    }
}