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
            return await context.Apartments.ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<Apartment> GetApartment([Service(ServiceKind.Resolver)] ApplicationDbContext context, int id)
        {
            var apartment = await context.Apartments.FindAsync(id);
            return apartment;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartmentsByDistrict([Service(ServiceKind.Resolver)] ApplicationDbContext context, string district)
        {
            return await context.Apartments
                .Where(a => a.District.ToLower().Contains(district.ToLower()))
                .ToListAsync();
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        [UseProjection]
        public async Task<List<Apartment>> GetApartmentsByPriceRange([Service(ServiceKind.Resolver)] ApplicationDbContext context, decimal minPrice, decimal maxPrice)
        {
            return await context.Apartments
                .Where(a => a.Price >= minPrice && a.Price <= maxPrice)
                .ToListAsync();
        }
    }
}