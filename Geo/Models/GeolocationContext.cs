using Microsoft.EntityFrameworkCore;

namespace Geo.Models
{
    public class GeolocationContext : DbContext
    {
        public DbSet<Geolocation> Geolocations { get; set; }       
        public GeolocationContext(DbContextOptions<GeolocationContext> options)
           : base(options)
        {
            Database.EnsureCreated();
        }
       
    }
}
