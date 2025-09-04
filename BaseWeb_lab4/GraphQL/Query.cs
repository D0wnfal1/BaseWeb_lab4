using BaseWeb_lab4.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BaseWeb_lab4.GraphQL
{
    public class Query
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartments([Service(ServiceKind.Resolver)] ApplicationDbContext context)
        {
            return await context.Apartments.Include(a => a.Owner).ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<Apartment> GetApartment([Service(ServiceKind.Resolver)] ApplicationDbContext context, int id)
        {
            var apartment = await context.Apartments
                .Include(a => a.Owner)
                .FirstOrDefaultAsync(a => a.Id == id);
            return apartment;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartmentsByDistrict([Service(ServiceKind.Resolver)] ApplicationDbContext context, string district)
        {
            return await context.Apartments
                .Include(a => a.Owner)
                .Where(a => a.District.ToLower().Contains(district.ToLower()))
                .ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartmentsByPriceRange([Service(ServiceKind.Resolver)] ApplicationDbContext context, decimal minPrice, decimal maxPrice)
        {
            return await context.Apartments
                .Include(a => a.Owner)
                .Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                .ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartmentsByOwnerName([Service(ServiceKind.Resolver)] ApplicationDbContext context, string ownerName)
        {
            return await context.Apartments
                .Include(a => a.Owner)
                .Where(a => a.Owner.Name.Contains(ownerName))
                .ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Owner>> GetOwnersByApartmentCount([Service(ServiceKind.Resolver)] ApplicationDbContext context, int minApartments)
        {
            return await context.Owners
                .Include(o => o.Apartments)
                .Where(o => o.Apartments.Count >= minApartments)
                .ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Owner>> GetOwners([Service(ServiceKind.Resolver)] ApplicationDbContext context)
        {
            return await context.Owners.Include(o => o.Apartments).ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<Owner> GetOwner([Service(ServiceKind.Resolver)] ApplicationDbContext context, int id)
        {
            var owner = await context.Owners
                .Include(o => o.Apartments)
                .FirstOrDefaultAsync(o => o.Id == id);
            return owner;
        }
    }
}