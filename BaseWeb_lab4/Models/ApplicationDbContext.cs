using Microsoft.EntityFrameworkCore;

namespace BaseWeb_lab4.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Apartment> Apartments { get; set; }
    }
}