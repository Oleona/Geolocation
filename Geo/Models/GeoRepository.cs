using Dadata.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Geo.Models
{
    public partial class GeoRepository : IRepository
    {
        private readonly GeolocationContext dbContext;
        public GeoRepository(GeolocationContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Geolocation> SearchByLatAndLonAsync(double latitude, double longitude) =>
            await dbContext.Geolocations.SingleOrDefaultAsync(x => x.Latitude == latitude && x.Longitude == longitude);

        // так как lat и lon  в address строки, то проще передать их не из него, а отдельно
        public async Task CreateNewPointAsync(double latitude, double longitude, Address address)
        {
            var newGeo = new Geolocation
            {
                Id = new Guid(),
                Latitude = latitude,
                Longitude = longitude,
                Country = address.country,
                RegionType = address.region_type_full,
                RegionName = address.region,
                CityType = address.city_type_full,
                CityName = address.city,
                StreetType = address.street_type_full,
                StreetName = address.street,
                HouseType = address.house_type_full,
                HouseNumber = address.house,
            };
            dbContext.Add(newGeo);
            await dbContext.SaveChangesAsync();
        }
    }
}
