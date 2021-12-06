using Microsoft.EntityFrameworkCore;
using Weather.Models;

namespace Weather.Contexts
{
    public class CityContext : DbContext
    {
        public CityContext (DbContextOptions<CityContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
    }
}
