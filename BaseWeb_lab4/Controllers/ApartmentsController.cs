using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BaseWeb_lab4.Models;

namespace BaseWeb_lab4.Controllers
{
    public class ApartmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ApartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string district, decimal? minPrice, decimal? maxPrice, int? minRooms, int? maxRooms)
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

            ViewBag.District = district;
            ViewBag.MinPrice = minPrice;
            ViewBag.MaxPrice = maxPrice;
            ViewBag.MinRooms = minRooms;
            ViewBag.MaxRooms = maxRooms;

            return View(await apartments.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Owners = await _context.Owners.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,District,Floor,Area,Rooms,OwnerId,Price")] Apartment apartment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(apartment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                return NotFound();
            }
            ViewBag.Owners = await _context.Owners.ToListAsync();
            return View(apartment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,District,Floor,Area,Rooms,OwnerId,Price")] Apartment apartment)
        {
            if (id != apartment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(apartment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApartmentExists(apartment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(apartment);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var apartment = await _context.Apartments
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (apartment == null)
            {
                return NotFound();
            }

            return View(apartment);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var apartment = await _context.Apartments.FindAsync(id);
            if (apartment != null)
            {
                _context.Apartments.Remove(apartment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApartmentExists(int id)
        {
            return _context.Apartments.Any(e => e.Id == id);
        }
    }
}