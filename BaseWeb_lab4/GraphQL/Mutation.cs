using BaseWeb_lab4.Models;

namespace BaseWeb_lab4.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<Apartment> CreateApartment(
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            string district,
            int floor,
            string area,
            int rooms,
            string owner,
            decimal price)
        {
            var apartment = new Apartment
            {
                District = district,
                Floor = floor,
                Area = area,
                Rooms = rooms,
                Owner = owner,
                Price = price
            };

            context.Apartments.Add(apartment);
            await context.SaveChangesAsync();

            return apartment;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<Apartment> UpdateApartment(
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            int id,
            string? district,
            int? floor,
            string? area,
            int? rooms,
            string? owner,
            decimal? price)
        {
            var apartment = await context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                throw new GraphQLException(new Error("Apartment not found", "APARTMENT_NOT_FOUND"));
            }

            if (district != null) apartment.District = district;
            if (floor.HasValue) apartment.Floor = floor.Value;
            if (area != null) apartment.Area = area;
            if (rooms.HasValue) apartment.Rooms = rooms.Value;
            if (owner != null) apartment.Owner = owner;
            if (price.HasValue) apartment.Price = price.Value;

            await context.SaveChangesAsync();

            return apartment;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<bool> DeleteApartment([Service(ServiceKind.Resolver)] ApplicationDbContext context, int id)
        {
            var apartment = await context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                throw new GraphQLException(new Error("Apartment not found", "APARTMENT_NOT_FOUND"));
            }

            context.Apartments.Remove(apartment);
            await context.SaveChangesAsync();

            return true;
        }

        [UseDbContext(typeof(ApplicationDbContext))]
        public async Task<Apartment> UpdateApartmentPrice(
            [Service(ServiceKind.Resolver)] ApplicationDbContext context,
            int id,
            decimal newPrice)
        {
            var apartment = await context.Apartments.FindAsync(id);
            if (apartment == null)
            {
                throw new GraphQLException(new Error("Apartment not found", "APARTMENT_NOT_FOUND"));
            }

            apartment.Price = newPrice;
            await context.SaveChangesAsync();

            return apartment;
        }
    }
}